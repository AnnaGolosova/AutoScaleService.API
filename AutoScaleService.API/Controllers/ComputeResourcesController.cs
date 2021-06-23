using AutoScaleService.AbstractQueue;
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
        private readonly ITasksQueue<RegisterTaskModel> _tasksQueue;

        public ComputeResourcesController(IMediator mediator,
            ITasksQueue<RegisterTaskModel> tasksQueue)
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
            if(registerTaskModel.TranslationsCount > 100000)
            {
                // todo
                return BadRequest("each request can include up to 100, 000 tasks");
            }
            // todo - add log to file
            // todo - add tasks separations
            var fullTasksCount = registerTaskModel.TranslationsCount % 30000;
            var remainTasks = registerTaskModel.TranslationsCount / 30000;

            _tasksQueue.TrySetNextTask(registerTaskModel);

            return Created(string.Empty, new RegisteredTaskResponse());
        }
    }
}
