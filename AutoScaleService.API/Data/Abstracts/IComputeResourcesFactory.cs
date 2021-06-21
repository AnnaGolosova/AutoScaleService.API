using AutoScaleService.API.Data.Contracts;

namespace AutoScaleService.API.Data.Abstracts
{
    public interface IComputeResourcesFactory<out TResourceType> where TResourceType : AbstractComputeResource
    {
        TResourceType Create();
    }
}
