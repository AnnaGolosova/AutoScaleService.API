using AutoScaleService.API.Data.Contracts;
using MediatR;

namespace AutoScaleService.API.Commands
{
    public class ComputeResourceReleasedCommand : IRequest<ComputeResource>
    {
        public ComputeResourceReleasedCommand(ComputeResource computeResource)
        {
            ComputeResource = computeResource;
        }

        public ComputeResource ComputeResource { get; set; }
    }
}
