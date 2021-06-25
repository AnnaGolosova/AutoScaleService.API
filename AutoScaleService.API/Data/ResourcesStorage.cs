using System;
using System.Collections.Concurrent;
using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoScaleService.Models.Configuration;
using AutoScaleService.Models.Notifications;
using AutoScaleService.Models.Tasks;
using AutoScaleService.Notifications;
using AutoScaleService.Notifications.Abstracts;

namespace AutoScaleService.API.Data
{
    public class ResourcesStorage : IResourcesStorage
    {
        private readonly IComputeResourcesFactory<ComputeResource> _resourcesFactory;
        private readonly IHostedService _hostedService;
        private readonly ResourcesSettings _resourcesSettings;
        private readonly ILogger<ResourcesStorage> _logger;
        private readonly INotificationsService _notificationsService;

        private readonly object _lockObject = new object();

        private readonly List<AbstractComputeResource> _resources = new List<AbstractComputeResource>();
        private readonly ConcurrentBag<Guid> _stoppedResourcesIds = new ConcurrentBag<Guid>();

        public ResourcesStorage(IHostedService hostedService, 
            IOptions<ResourcesSettings> resourcesSettings, 
            IComputeResourcesFactory<ComputeResource> resourcesFactory,
            ILogger<ResourcesStorage> logger, 
            INotificationsService notificationsService)
        {
            _hostedService = hostedService;
            _resourcesSettings = resourcesSettings.Value;
            _resourcesFactory = resourcesFactory;
            _logger = logger;
            _notificationsService = notificationsService;
        }

        public int GetIdleResourcesCount() => _resources.Where(r => !r.IsBusy).ToList().Count;

        public int GetAvailableToStartResourcesCount() => _resourcesSettings.MaxCount - _resources.Count;

        public void Execute(RegisterTasksRequestDto model)
        {
            var resourcesCountToProcess = model.TranslationTasksCount % _resourcesSettings.ResourceTranslationRate;

            var idleResourcesCount = GetIdleResourcesCount();

            var countToCreate = resourcesCountToProcess - idleResourcesCount; 

            if(idleResourcesCount < resourcesCountToProcess && _resourcesSettings.MaxCount >= countToCreate + _resources.Count)
            {
                _logger.LogInformation($"Starts creating {countToCreate} resources");

                for (; countToCreate > 0; countToCreate--)
                {
                    var newResource = _resourcesFactory.Create(model.NotificationUrl);

                    lock (_lockObject)
                    {
                        _resources.Add(newResource);
                    }
                }
            }

            List<AbstractComputeResource> resourcesToStart;

            lock (_lockObject)
            {
                resourcesToStart = _resources.Where(r => !r.IsBusy).Take(resourcesCountToProcess).ToList();
                _logger.LogInformation($"Start {resourcesToStart.Count} compute resources");
            }
            
            _ = resourcesToStart.Select(r => Task.Run(() => r.Invoke(new ExecutableTask(Guid.NewGuid(), model.RequestId)))).ToList();
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
        {
            lock (_lockObject)
            {
                var resourceToRemove = _resources.Find(r => r.ExecutableTask?.Id == computeResource.ExecutableTask.Id);

                _resources.Remove(resourceToRemove);

                var requestId = computeResource.ExecutableTask.RequestId;

                if (!_stoppedResourcesIds.Contains(computeResource.Id) && _resources.Count(r => r.ExecutableTask?.RequestId == requestId) == 0)
                {
                    var notification = new Notification(requestId, "Success");

                    _notificationsService.SendNotification(notification, computeResource.NotificationUrl);

                    _stoppedResourcesIds.Add(computeResource.Id);
                }

                _logger.LogInformation($"Compute resource with id {computeResource.Id} was removed");

                if (_resources.Count == 0 || _resources.Any(r => !r.IsBusy))
                {
                    _hostedService.StartAsync(CancellationToken.None);
                }
            }
        }
    }
}
