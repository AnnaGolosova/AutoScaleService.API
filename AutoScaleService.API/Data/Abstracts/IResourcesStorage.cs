using AutoScaleService.API.Data.Contracts;

namespace AutoScaleService.API.Data.Abstracts
{
    public interface IResourcesStorage
    {
        void StartTaskExecution(int requestedResourcesCount, ExecutableTask task);

        int GetAvaliableResourcesCount();
    }
}
