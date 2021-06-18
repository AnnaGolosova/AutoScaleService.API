﻿using AutoScaleService.API.Data.Contracts;

namespace AutoScaleService.API.Services.Abstracts
{
    public interface IComputeResourcesManager
    {
        bool CanProcessTask(int estimatedTaskDuration);

        void ProcessNextTask(WorkItem workItem);

        void ReleaseComputeResource(AbstractComputeResource computeResource);
    }
}