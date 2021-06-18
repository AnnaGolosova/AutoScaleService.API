using AutoScaleService.API.Data.Contracts;

namespace AutoScaleService.API.Data.Abstracts
{
    public interface IResourcesStorage
    {
        void Execute(int requestedResourcesCount, ExecutableTask task);

        int GetAvailableResourcesCount();

        void ReleaseComputeResource(AbstractComputeResource computeResource);
    }
}
