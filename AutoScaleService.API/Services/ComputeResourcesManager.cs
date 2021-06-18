using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using MediatR;
using System;

namespace AutoScaleService.API.Services
{
    public class ComputeResourcesManager : IComputeResourcesManager
    {
        private readonly IMediator _mediator;
        private readonly IResourcesStorage _resourcesStorage;

        public ComputeResourcesManager(IMediator mediator, IResourcesStorage resourcesStorage)
        {
            _mediator = mediator;
            _resourcesStorage = resourcesStorage;
        }

        public void ProcessNextTask(WorkItem workItem)
        {
            if(workItem == null)
            {
                throw new ArgumentNullException(nameof(WorkItem));
            }
            if(workItem.Task == null)
            {
                throw new ArgumentNullException(nameof(workItem.Task));
            }
            _resourcesStorage.Execute(workItem.RequestedResourcesCount, workItem.Task);
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
            => _resourcesStorage.ReleaseComputeResource(computeResource);

        public bool CanProcessTask(int estimatedTaskDuration)
            => _resourcesStorage.GetAvailableResourcesCount() >= estimatedTaskDuration;
    }
}
