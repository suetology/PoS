using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Shift;
using PoS.WebApi.Application.Services.Shift.Contracts;

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

        [HttpGet]
        public async Task<IActionResult> GetShifts(GetShiftsRequest request)
        {
            var response = await _shiftService.GetShifts(request);
            
            return Ok(response);
        }

        [HttpGet("{shiftId}")]
        public async Task<IActionResult> GetShiftById(Guid shiftId)
        {
            var response = await _shiftService.GetShift(shiftId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftRequest request)
        {
            if (request == null)
            {
                return BadRequest("Shift data is null.");
            }

            await _shiftService.CreateShift(request);
            
            return CreatedAtAction(nameof(GetShiftById), request);
        }

        [HttpDelete("{shiftId}")]
        public async Task<IActionResult> DeleteShift(Guid shiftId)
        {
            await _shiftService.DeleteShift(shiftId);
            
            return NoContent();
        }
    }
}
