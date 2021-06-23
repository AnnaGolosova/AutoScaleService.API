using AutoScaleService.API.Data.Contracts;
using AutoScaleService.Models.Request;

namespace AutoScaleService.API.Data.Abstracts
{
    public interface IResourcesStorage
    {
        void Execute(RegisterTaskModel model);

        int GetAvailableToCreateResourcesCount();

        void ReleaseComputeResource(AbstractComputeResource computeResource);
    }
}
