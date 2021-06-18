using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoScaleService.Models.ResourcesSettings;

namespace AutoScaleService.API.Data
{
    public class ResourcesStorage : IResourcesStorage
    {
        private readonly IComputeResourcesFactory<ComputeResource> _resourcesFactory;
        private readonly IHostedService _hostedService;
        private readonly ResourcesSettings _resourcesSettings;

        private readonly List<AbstractComputeResource> _resources;

        public ResourcesStorage(IConfiguration configuration, IHostedService hostedService, ResourcesSettings resourcesSettings, IComputeResourcesFactory<ComputeResource> resourcesFactory)
        {
            _hostedService = hostedService;
            _resourcesSettings = resourcesSettings;
            _resourcesFactory = resourcesFactory;

            _resources = new List<AbstractComputeResource>();
        }

        public int GetAvailableResourcesCount() => _resources.Count(r => !r.isBusy) + _resourcesSettings.MaxCount - _resources.Count();

        public void Execute(int requestedResourcesCount, ExecutableTask task)
        {
            var countToCreate = requestedResourcesCount - GetAvailableResourcesCount();

            if(GetAvailableResourcesCount() < requestedResourcesCount && _resourcesSettings.MaxCount > countToCreate + _resources.Count)
            {
                for(; countToCreate > 0; countToCreate--)
                {
                    var newResource = _resourcesFactory.CreateComputeResource();

                    _resources.Add(newResource);
                }
            }

            List<AbstractComputeResource> resourcesForTaskProcessing = _resources.Where(r => !r.isBusy).Take(requestedResourcesCount).ToList();

            resourcesForTaskProcessing.ForEach(r => r.Invoke(task));
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
        {
            // To Do add locks to _resources collection
            List<AbstractComputeResource> resource = _resources.FindAll(r => r.Task.Id == computeResource.Task.Id);

            resource.ForEach(r =>
            {
                r.Release();
            });

            if (_resources.Count == 0 || _resources.All(r => !r.isBusy))
            {
                _hostedService.StartAsync(new CancellationToken());
            }
        }
    }
}
