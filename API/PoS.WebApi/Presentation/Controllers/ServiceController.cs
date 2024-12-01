using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Service;
using PoS.WebApi.Application.Services.Service.Contracts;

namespace PoS.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetService(Guid serviceId)
        {
            var response = await _serviceService.GetService(serviceId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetServices([FromQuery] string sort = "name", [FromQuery] string order = "desc", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _serviceService.GetServices(sort, order, page, pageSize);
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceRequest request)
        {
            await _serviceService.CreateService(request);
            
            return CreatedAtAction(nameof(GetService), request);
        }

        [HttpPatch("{serviceId}")]
        public async Task<IActionResult> UpdateService(Guid serviceId, [FromBody] UpdateServiceRequest request)
        {
            await _serviceService.UpdateService(serviceId, request);
            
            return NoContent();
        }
    }
}
