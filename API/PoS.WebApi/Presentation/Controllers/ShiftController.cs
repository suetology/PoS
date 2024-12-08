using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Shift;
using PoS.WebApi.Application.Services.Shift.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("shifts")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet]
        public async Task<IActionResult> GetShifts([FromQuery] QueryParameters parameters)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new GetAllShiftsRequest
            {
                BusinessId = businessId.Value,
                QueryParameters = parameters
            };
            
            var response = await _shiftService.GetShifts(request);
            
            return Ok(response);
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpGet("{shiftId}")]
        public async Task<IActionResult> GetShiftById(Guid shiftId)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new GetShiftRequest
            {
                Id = shiftId,
                BusinessId = businessId.Value
            };
            
            var response = await _shiftService.GetShift(request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftRequest request)
        {
            if (request == null)
            {
                return BadRequest("Shift data is null.");
            }
            
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            request.BusinessId = businessId.Value;

            await _shiftService.CreateShift(request);
            
            return CreatedAtAction(nameof(CreateShift), request);
        }

        [HttpDelete("{shiftId}")]
        public async Task<IActionResult> DeleteShift(Guid shiftId)
        {
            var businessId = User.GetBusinessId();

            if (businessId == null)
            {
                return Unauthorized("Failed to retrieve Business ID");
            }

            var request = new DeleteShiftRequest
            {
                Id = shiftId,
                BusinessId = businessId.Value
            };
            
            await _shiftService.DeleteShift(request);
            
            return NoContent();
        }
    }
}
