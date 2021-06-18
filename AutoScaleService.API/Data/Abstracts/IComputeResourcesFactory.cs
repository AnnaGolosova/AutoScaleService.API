using AutoScaleService.API.Data.Contracts;

namespace AutoScaleService.API.Data.Abstracts
{
    public interface IComputeResourcesFactory
    {
        IResourceType Create<IResourceType>() where IResourceType : AbstractComputeResource, new();
    }
}
