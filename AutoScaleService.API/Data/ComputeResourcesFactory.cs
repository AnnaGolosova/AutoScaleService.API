using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoScaleService.API.Data
{
    public class ComputeResourcesFactory : IComputeResourcesFactory<ComputeResource>
    {
        private readonly ILogger<ComputeResourcesFactory> _logger;
        private readonly IMediator _mediator;

        public ComputeResourcesFactory(ILogger<ComputeResourcesFactory> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //public TResourceType Create<TResourceType>() where TResourceType : AbstractComputeResource, new()
        //{
        //    _logger.LogInformation("New abstract compute resource was created!");

        //    return new TResourceType();
        //}

        public ComputeResource CreateComputeResource()
        {
            // TO DO change factory to be generic
            _logger.LogInformation("New compute resource was created!");

            return new ComputeResource(_mediator);
        }
    }
}
