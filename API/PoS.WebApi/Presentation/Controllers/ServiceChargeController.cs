using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.ServiceCharge;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
        [HttpPost]
        public async Task<IActionResult> CreateServiceCharge([FromBody] CreateServiceChargeRequest request)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            request.BusinessId = businessId.Value;
            
            await _serviceChargeService.CreateServiceCharge(request);
            
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceChargeDto>>> GetServiceCharges()
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new GetAllServiceChargesRequest
            {
                BusinessId = businessId.Value
            };
            
            var response = await _serviceChargeService.GetServiceCharges(request);
            return Ok(response);
        }
    }
}
    