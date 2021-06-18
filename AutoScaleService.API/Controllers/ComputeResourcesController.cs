using AutoScaleService.Models.Request;
using AutoScaleService.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoScaleService.API.Controllers
{
    [ApiController]
    [Route("compute-resources")]
    public class ComputeResourcesController : ControllerBase
    {
        private readonly ILogger<ComputeResourcesController> _logger;

        public ComputeResourcesController(ILogger<ComputeResourcesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(1);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTaskAsync([FromBody]RegisterTaskModel registerTaskModel)
        {
            return Created(string.Empty, new RegisteredTaskResponse());
        }
    }
}
