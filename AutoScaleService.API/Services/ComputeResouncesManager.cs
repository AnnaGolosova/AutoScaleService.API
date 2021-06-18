using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using MediatR;
using System;

namespace AutoScaleService.API.Services
{
    public class ComputeResouncesManager : IComputeResouncesManager
    {
        private readonly IMediator _mediator;
        private readonly IResourcesStorage _resourcesStorage;

        public ComputeResouncesManager(IMediator mediator)
        {
            _mediator = mediator;
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
            _resourcesStorage.StartTaskExecution(workItem.RequestedResourcesCount, workItem.Task);
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
            => _resourcesStorage.ReleaseComputeResource(computeResource);

        public bool CanProcessTask(int estimatedTaskDuration)
            => _resourcesStorage.GetAvaliableResourcesCount() >= estimatedTaskDuration;
    }
}
