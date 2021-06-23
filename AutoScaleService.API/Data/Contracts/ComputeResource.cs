using AutoScaleService.API.Commands;
using MediatR;

namespace AutoScaleService.API.Data.Contracts
{
    public class ComputeResource :  AbstractComputeResource
    {
        private readonly IMediator _mediator;

        public ComputeResource(IMediator mediator) :  base()
        {
            _mediator = mediator;
        }

        public override void Invoke(ExecutableTask task)
        {
            Task = task;

            base.Invoke(task);

            _mediator.Send(new ComputeResourceReleasedCommand()
            { 
                ComputeResource = this
            });
        }
    }
}
