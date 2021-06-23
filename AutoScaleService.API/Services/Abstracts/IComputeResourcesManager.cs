using AutoScaleService.API.Data.Contracts;
using AutoScaleService.Models.Request;

namespace AutoScaleService.API.Services.Abstracts
{
    public interface IComputeResourcesManager
    {
        bool CanProcessTask(int estimatedTaskDuration);

        void ProcessNextTask(RegisterTaskModel workItem);

        void ReleaseComputeResource(AbstractComputeResource computeResource);
    }
}
