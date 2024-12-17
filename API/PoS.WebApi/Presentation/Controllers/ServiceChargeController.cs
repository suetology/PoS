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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        [ProducesResponseType(typeof(GetAllServiceChargesResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
        [HttpPatch("{serviceChargeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateServiceCharge([FromRoute] Guid serviceChargeId, [FromBody] UpdateServiceChargeRequest request)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            request.Id = serviceChargeId;
            request.BusinessId = businessId.Value;

            await _serviceChargeService.UpdateServiceCharge(request);

            return NoContent();
        }

    }
}
    