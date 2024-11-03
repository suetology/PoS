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
        public async Task<IActionResult> GetShifts([FromQuery] Guid? employeeId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var shifts = await _shiftService.GetShifts(employeeId, fromDate, toDate);
            return Ok(new { shifts });
        }

        [HttpGet("{shiftId}")]
        public async Task<IActionResult> GetShiftById(Guid shiftId)
        {
            var shift = await _shiftService.GetShift(shiftId);
            if (shift == null)
            {
                return NotFound();
            }
            return Ok(shift);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] ShiftDto shiftDto)
        {
            if (shiftDto == null)
            {
                return BadRequest("Shift data is null.");
            }

            await _shiftService.CreateShift(shiftDto);
            return CreatedAtAction(nameof(GetShiftById), new { shiftId = shiftDto.ToDomain().Id }, shiftDto);
        }

        [HttpDelete("{shiftId}")]
        public async Task<IActionResult> DeleteShift(Guid shiftId)
        {
            await _shiftService.DeleteShift(shiftId);
            return NoContent();
        }
    }
}
