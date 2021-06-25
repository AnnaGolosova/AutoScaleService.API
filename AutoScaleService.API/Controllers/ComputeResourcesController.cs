using AutoScaleService.API.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoScaleService.API.Commands;
using AutoScaleService.Models.Tasks;

namespace AutoScaleService.API.Controllers
{
    [ApiController]
    [Route("api/compute-resources")]
    public class ComputeResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComputeResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available-resources-count")]
        public async Task<IActionResult> GetAvailableResourcesCount()
        {
            var result = await _mediator.Send(new GetAvailableResourcesCountQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterTasksRequestDto registerTasksRequest)
        {
            if(registerTasksRequest.TranslationTasksCount > 100000)
            {
                return BadRequest("Maximum tasks count (100 000) exceeded.");
            }

            await _mediator.Send(new RegisterTaskCommand(registerTasksRequest));

            return Accepted();
        }
    }
}
