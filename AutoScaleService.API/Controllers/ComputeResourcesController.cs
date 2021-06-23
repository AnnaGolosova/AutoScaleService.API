using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Query;
using AutoScaleService.Models.Request;
using AutoScaleService.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AutoScaleService.API.Controllers
{
    [ApiController]
    [Route("api/compute-resources")]
    public class ComputeResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITasksQueue<WorkItem> _tasksQueue;

        public ComputeResourcesController(IMediator mediator,
            ITasksQueue<WorkItem> tasksQueue)
        {
            _mediator = mediator;
            _tasksQueue = tasksQueue;
        }

        [HttpGet("available-resources-count")]
        public async Task<IActionResult> GetAvailableResourcesCount()
        {
            var result = await _mediator.Send(new GetAvailableResourcesCountQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterTaskModel registerTaskModel)
        {
            var newGuid = Guid.NewGuid();
            _tasksQueue.TrySetNextTask(new WorkItem() { 
                TaskId = newGuid,
                TranslationTasksCount = registerTaskModel.TranslationTasksCount,
                Task = new ExecutableTask(newGuid, 5, registerTaskModel.RedirectUrl)
            });

            return Created(string.Empty, new RegisteredTaskResponse());
        }
    }
}
