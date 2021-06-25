using System;
using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using MediatR;
using Serilog;

namespace AutoScaleService.API.Data
{
    public class ComputeResourcesFactory : IComputeResourcesFactory<ComputeResource>
    {
        private readonly IMediator _mediator;

        public ComputeResourcesFactory(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ComputeResource Create(string notificationUrl)
        {
            var resourceId = Guid.NewGuid();

            Log.Logger.Information($"New compute resource with id {resourceId} was created!");

            return new ComputeResource(_mediator, notificationUrl, resourceId);
        }
    }
}
