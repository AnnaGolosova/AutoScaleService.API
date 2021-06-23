using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.Models.Request;
using AutoScaleService.Models.ResourcesSettings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoScaleService.API.Data
{
    public class ResourcesStorage : IResourcesStorage
    {
        private readonly IComputeResourcesFactory<ComputeResource> _resourcesFactory;
        private readonly IHostedService _hostedService;
        private readonly ResourcesSettings _resourcesSettings;
        private readonly ILogger<ResourcesStorage> _logger;

        private object _lockObject = new object();

        private readonly List<AbstractComputeResource> _resources = new List<AbstractComputeResource>();

        public ResourcesStorage(IHostedService hostedService, 
            IOptions<ResourcesSettings> resourcesSettings, 
            IComputeResourcesFactory<ComputeResource> resourcesFactory,
            ILogger<ResourcesStorage> logger)
        {
            _hostedService = hostedService;
            _resourcesSettings = resourcesSettings.Value;
            _resourcesFactory = resourcesFactory;
            _logger = logger;
        }

        private int GetAvailableResourcesCount() => _resources.Count(r => !r.isBusy);

        public int GetAvailableToCreateResourcesCount() => GetAvailableResourcesCount() + (_resourcesSettings.MaxCount - _resources.Count);

        public void Execute(RegisterTaskModel model)
        {
            var machineCountToProcess = model.TranslationsCount / _resourcesSettings.ResourceTranslationRate;

            var countToCreate = machineCountToProcess - GetAvailableResourcesCount(); 

            if(GetAvailableResourcesCount() < machineCountToProcess && _resourcesSettings.MaxCount >= countToCreate + _resources.Count)
            {
                _logger.LogInformation($"Starts creating {countToCreate} resources");
                for (; countToCreate > 0; countToCreate--)
                {
                    var newResource = _resourcesFactory.Create();

                    lock (_lockObject)
                    {
                        _resources.Add(newResource);
                    }
                }
            }

            List<AbstractComputeResource> resourcesForTaskProcessing;
            lock (_lockObject)
            {
                resourcesForTaskProcessing = _resources.Where(r => !r.isBusy).Take(machineCountToProcess).ToList();
            }
            _logger.LogInformation($"Starts execution {resourcesForTaskProcessing.Count} tasks");

            // ToDo - add guid usage here

            _ = resourcesForTaskProcessing.Select(r => Task.Run(() => r.Invoke(new ExecutableTask(System.Guid.Empty, model.RedirectUrl)))).ToList();
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
        {
            lock (_lockObject)
            {
                // To Do add locks to _resources collection
                var resource = _resources.Where(r => r.Task?.Id == computeResource.Task.Id).ToList();

                _logger.LogInformation($"Removing {computeResource.Task.Id} resources");

                resource.ForEach(r =>
                {
                    _resources.Remove(r);
                    //r.Release();
                });

                if (_resources.Count == 0 || _resources.Any(r => !r.isBusy))
                {
                    _hostedService.StartAsync(CancellationToken.None);
                }
            }
        }
    }
}
