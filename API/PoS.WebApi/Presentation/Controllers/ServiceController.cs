using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Service;
using PoS.WebApi.Application.Services.Service.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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
        
        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet("{serviceId}")]
        [ProducesResponseType(typeof(GetServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetService(Guid serviceId)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new GetServiceRequest
            {
                Id = serviceId,
                BusinessId = businessId.Value
            };
            
            var response = await _serviceService.GetService(request);

            return Ok(response);
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAllServicesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServices([FromQuery] string sort = "name", [FromQuery] string order = "desc", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new GetAllServicesRequest
            {
                BusinessId = businessId.Value,
                Sort = sort,
                Order = order,
                Page = page,
                PageSize = pageSize
            };
            
            var response = await _serviceService.GetServices(request);
            
            return Ok(response);
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet("active")]
        [ProducesResponseType(typeof(GetAllServicesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActiveServices([FromQuery] string sort = "name", [FromQuery] string order = "desc", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new GetAllServicesRequest
            {
                BusinessId = businessId.Value,
                Sort = sort,
                Order = order,
                Page = page,
                PageSize = pageSize
            };
            
            var response = await _serviceService.GetActiveServices(request);
            
            return Ok(response);
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
        [HttpPost]
        [ProducesResponseType(typeof(ServiceDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]



        public async Task<IActionResult> CreateService([FromBody] CreateServiceRequest request)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            request.BusinessId = businessId.Value;
            
            await _serviceService.CreateService(request);
            
            return CreatedAtAction(nameof(CreateService), request);
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
        [HttpPatch("{serviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateService(Guid serviceId, [FromBody] UpdateServiceRequest request)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            request.Id = serviceId;
            request.BusinessId = businessId.Value;
            
            await _serviceService.UpdateService(request);
            
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet("{serviceId}/available-times")]
        public async Task<IActionResult> GetAvailableTimes([FromRoute] Guid serviceId, [FromQuery] DateTime date)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            } 

            var request = new GetAvailableTimesRequest
            {
                Id = serviceId,
                BusinessId = businessId.Value,
                Date = date
            };

            var response = await _serviceService.GetAvailableTimes(request);

            return Ok(response);
        }
    }
}
