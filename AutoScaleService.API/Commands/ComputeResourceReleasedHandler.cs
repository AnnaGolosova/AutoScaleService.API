using AutoScaleService.API.Data.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoScaleService.API.Commands
{
    public class ComputeResourceReleasedHandler : IRequestHandler<ComputeResourceReleasedCommand, ComputeResource>
    {
        public Task<ComputeResource> Handle(ComputeResourceReleasedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
