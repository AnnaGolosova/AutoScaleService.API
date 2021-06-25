using AutoScaleService.API.Commands;
using MediatR;

namespace AutoScaleService.API.Data.Contracts
{
    public class ComputeResource :  AbstractComputeResource
    {
        private readonly IMediator _mediator;

        public ComputeResource(IMediator mediator, string notificationUrl) : base(notificationUrl)
        {
            _mediator = mediator;
        }

        public override void Invoke(ExecutableTask task)
        {
            ExecutableTask = task;

            base.Invoke(task);

            _mediator.Send(new ComputeResourceReleasedCommand(this));
        }
    }
}
