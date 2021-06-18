using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AutoScaleService.API.Commands
{
    public class ComputeResourceReleasedHandler : IRequestHandler<ComputeResourceReleasedCommand, ComputeResource>
    {
        private readonly IComputeResouncesManager _computeResouncesManager;

        public ComputeResourceReleasedHandler(IComputeResouncesManager computeResouncesManager)
        {
            _computeResouncesManager = computeResouncesManager;
        }

        public async Task<ComputeResource> Handle(ComputeResourceReleasedCommand request, CancellationToken cancellationToken)
        {
            _computeResouncesManager.ReleaseComputeResource(request.ComputeResource);

            return request.ComputeResource;
        }
    }
}
