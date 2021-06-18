using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using MediatR;

namespace AutoScaleService.API.Services
{
    public class ComputeResouncesManager : IComputeResouncesManager
    {
        private readonly IMediator _mediator;

        public ComputeResouncesManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public bool TryToProcessNextTask(WorkItem workItem)
        //{

        //}

        public void Initiate()
        {

        }
    }
}
