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
            var service = await _serviceService.GetService(serviceId);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpGet]
        public async Task<IActionResult> GetServices([FromQuery] string sort = "name", [FromQuery] string order = "desc", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var services = await _serviceService.GetServices(sort, order, page, pageSize);
            return Ok(new { services });
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceDto serviceDto)
        {
            await _serviceService.CreateService(serviceDto);
            return CreatedAtAction(nameof(GetService), new { serviceId = serviceDto.EmployeeId }, serviceDto);
        }

        [HttpPatch("{serviceId}")]
        public async Task<IActionResult> UpdateService(Guid serviceId, [FromBody] ServiceDto serviceDto)
        {
            await _serviceService.UpdateService(serviceId, serviceDto);
            return NoContent();
        }
    }
}
