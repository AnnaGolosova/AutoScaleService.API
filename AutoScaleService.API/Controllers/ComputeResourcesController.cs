using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Services.Abstracts;
using AutoScaleService.Models.Request;
using AutoScaleService.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AutoScaleService.API.Controllers
{
    [ApiController]
    [Route("compute-resources")]
    public class ComputeResourcesController : ControllerBase
    {
        private readonly ILogger<ComputeResourcesController> _logger;
        private readonly IComputeResourcesManager _computeResourcesManager;
        private readonly IResourcesStorage _resourcesStorage;

        public ComputeResourcesController(
            ILogger<ComputeResourcesController> logger,
            IResourcesStorage resourcesStorage, IComputeResourcesManager computeResourcesManager)
        {
            _logger = logger;
            _resourcesStorage = resourcesStorage;
            _computeResourcesManager = computeResourcesManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new ResourcesCountResponse(_resourcesStorage.GetAvailableResourcesCount()));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTaskAsync([FromBody]RegisterTaskModel registerTaskModel)
        {
            // Add summary comments everywere
            return Created(string.Empty, new RegisteredTaskResponse());
        }
    }
}
