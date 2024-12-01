using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.ServiceCharge;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;

namespace PoS.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("serviceCharge")]
    public class ServiceChargeController : ControllerBase
    {
        private readonly IServiceChargeService _serviceChargeService;

        public ServiceChargeController(IServiceChargeService serviceChargeService)
        {
            _serviceChargeService = serviceChargeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceCharge([FromBody] CreateServiceChargeRequest request)
        {
            await _serviceChargeService.CreateServiceCharge(request);
            
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceChargeDto>>> GetServiceCharges()
        {
            var response = await _serviceChargeService.GetServiceCharges();
            return Ok(response);
        }
    }
}
    