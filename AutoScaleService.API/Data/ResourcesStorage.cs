using AutoScaleService.API.Data.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AutoScaleService.API.Data
{
    public class ResourcesStorage
    {
        private readonly ComputeResourcesFactory _resourcesFactory;

        public readonly int MaxResourcesCount;

        private List<AbstractComputeResource> _resources;

        public ResourcesStorage(int maxResourcesCount,
            ComputeResourcesFactory resourcesFabric)
        {
            _resourcesFactory = resourcesFabric;
            MaxResourcesCount = maxResourcesCount;

            _resources = new List<AbstractComputeResource>();
        }

        public int GetAvaliableResourcesCount()
            => _resources.Count(r => !r.isBusy);

        public void StartTaskExecution(int requestedResourcesCount, ExecutableTask task)
        {
            if(GetAvaliableResourcesCount() < requestedResourcesCount && MaxResourcesCount > requestedResourcesCount - GetAvaliableResourcesCount() + _resources.Count)
            {
                var countToCreate = requestedResourcesCount - GetAvaliableResourcesCount();

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
