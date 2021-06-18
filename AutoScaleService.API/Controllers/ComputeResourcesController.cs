using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Services.Abstracts;
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
        private readonly IComputeResouncesManager _computeResouncesManager;
        private readonly IResourcesStorage _resourcesStorage;

        public ComputeResourcesController(
            ILogger<ComputeResourcesController> logger,
            IResourcesStorage resourcesStorage,
            IComputeResouncesManager computeResouncesManager)
        {
            _logger = logger;
            _resourcesStorage = resourcesStorage;
            _computeResouncesManager = computeResouncesManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new ResourcesCountResponse(_resourcesStorage.GetAvaliableResourcesCount()));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTaskAsync([FromBody]RegisterTaskModel registerTaskModel)
        {
            return Created(string.Empty, new RegisteredTaskResponse());
        }
    }
}
