using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutoScaleService.API.Data
{
    public class ResourcesStorage : IResourcesStorage
    {
        private readonly IComputeResourcesFactory _resourcesFactory;
        private readonly IHostedService _hostedService;

        public readonly int MaxResourcesCount;

        private List<AbstractComputeResource> _resources;

        public ResourcesStorage(IConfiguration mySettings,
            IComputeResourcesFactory resourcesFabric,
            IHostedService hostedService)
        {
            _resourcesFactory = resourcesFabric;
            MaxResourcesCount = mySettings.GetValue<int>("MaxResourcesCount");
            _hostedService = hostedService;

            _resources = new List<AbstractComputeResource>();
        }

        public int GetAvaliableResourcesCount()
            => _resources.Count(r => !r.isBusy) + MaxResourcesCount - _resources.Count();

        public void StartTaskExecution(int requestedResourcesCount, ExecutableTask task)
        {
            var countToCreate = requestedResourcesCount - GetAvaliableResourcesCount();

            if(GetAvaliableResourcesCount() < requestedResourcesCount && MaxResourcesCount > countToCreate + _resources.Count)
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
            var resource = _resources.FindAll(r => r.Task.Id == computeResource.Task.Id);

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
