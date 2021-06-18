using AutoScaleService.API.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace AutoScaleService.API.Data
{
    public class ComputeResourcesFactory
    {
        private readonly ILogger<ComputeResourcesFactory> _logger;

        public ComputeResourcesFactory(ILogger<ComputeResourcesFactory> logger)
        {
            _logger = logger;
        }

        public IResourceType Create<IResourceType>() where IResourceType : AbstractComputeResource, new()
        {
            _logger.LogInformation("New coumpute resource was created!");

            return new IResourceType();
        }
    }
}
