using AutoScaleService.API.Data.Contracts;
using AutoScaleService.Models.Tasks;

namespace AutoScaleService.API.Services.Abstracts
{
    public interface IComputeResourcesManager
    {
        bool CanProcessTask(int estimatedTaskDuration);

        void ProcessNextTask(RegisterTasksRequestDto tasksRequestDto);

        void ReleaseComputeResource(AbstractComputeResource computeResource);
    }
}
