using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Services.Abstracts;
using AutoScaleService.Models.Request;
using AutoScaleService.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoScaleService.API.Query;
using MediatR;

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
        public async Task<IActionResult> RegisterTaskAsync([FromBody] RegisterTaskModel registerTaskModel)
        {
            return Created(string.Empty, new RegisteredTaskResponse());
        }
    }
}
