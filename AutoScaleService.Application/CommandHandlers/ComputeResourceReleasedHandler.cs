using System.Threading;
using System.Threading.Tasks;
using AutoScaleService.API.Commands;
using MediatR;

namespace AutoScaleService.Application.CommandHandlers
{
    public class ComputeResourceReleasedHandler : IRequestHandler<ComputeResourceReleasedCommand, ComputeResource>
    {
        private readonly IComputeResourcesManager _computeResourcesManager;

        public ComputeResourceReleasedHandler(IComputeResourcesManager computeResourcesManager)
        {
            _computeResourcesManager = computeResourcesManager;
        }

        public async Task<ComputeResource> Handle(ComputeResourceReleasedCommand request, CancellationToken cancellationToken)
        {
            _computeResourcesManager.ReleaseComputeResource(request.ComputeResource);

            return request.ComputeResource;
        }
    }
}
