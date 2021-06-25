using AutoScaleService.API.Data.Contracts;
using AutoScaleService.Models.Tasks;

namespace AutoScaleService.API.Data.Abstracts
{
    public interface IResourcesStorage
    {
        void Execute(RegisterTasksRequestDto model);

        int GetAvailableToStartResourcesCount();
        int GetIdleResourcesCount();

        void ReleaseComputeResource(AbstractComputeResource computeResource);
    }
}
