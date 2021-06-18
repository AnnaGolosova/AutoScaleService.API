using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AutoScaleService.API.Data
{
    public class ResourcesStorage : IResourcesStorage
    {
        private readonly IComputeResourcesFactory _resourcesFactory;

        public readonly int MaxResourcesCount;

        private List<AbstractComputeResource> _resources;

        public ResourcesStorage(IConfiguration mySettings,
            IComputeResourcesFactory resourcesFabric)
        {
            _resourcesFactory = resourcesFabric;
            MaxResourcesCount = mySettings.GetValue<int>("MaxResourcesCount");

            _resources = new List<AbstractComputeResource>();
        }

        public int GetAvaliableResourcesCount()
            => _resources.Count(r => !r.isBusy);

        public void StartTaskExecution(int requestedResourcesCount, ExecutableTask task)
        {
            var countToCreate = requestedResourcesCount - GetAvaliableResourcesCount();

            if(GetAvaliableResourcesCount() < requestedResourcesCount && MaxResourcesCount > countToCreate + _resources.Count)
            {
                for(; countToCreate > 0; countToCreate--)
                {
                    var newResource = _resourcesFactory.Create<ComputeResource>();

                    _resources.Add(newResource);
                }
            }

            List<AbstractComputeResource> resourcesForTaskProcessing = _resources.Where(r => !r.isBusy).Take(requestedResourcesCount).ToList();

            resourcesForTaskProcessing.ForEach(r => r.Invoke(task));
        }
    }
}
