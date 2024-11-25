using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Reservation;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    // Test Endpoints
    [HttpPost("test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTestReservation()
    {
        var reservationDto = new ReservationDto
        {
            AppointmentTime = DateTime.UtcNow.AddDays(1).Date.AddHours(10), // Tomorrow at 10 AM
            CustomerId = Guid.Parse("a7bc3a44-7f1a-4d9a-9c90-2d40fc56f5f3"), // Test Customer ID
            EmployeeId = Guid.Parse("f8fb2cb2-6119-4f42-8954-412e8bdc2351"), // Test Employee ID
        };

        try
        {
            await _reservationService.CreateReservation(reservationDto);
            return Ok(new { message = "Test reservation created successfully", reservation = reservationDto });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("test/availability")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TestAvailability()
    {
        var employeeId = Guid.Parse("f8fb2cb2-6119-4f42-8954-412e8bdc2351");
        var tomorrow = DateTime.UtcNow.AddDays(1).Date;
        
        var times = await _reservationService.GetAvailableTimesForEmployee(employeeId, tomorrow);
        return Ok(new
        {
            employeeId,
            date = tomorrow,
            availableSlots = times
        });
    }

    // Regular Endpoints
    [HttpPost(Name = nameof(CreateReservation))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    {
        try
        {
            if (!reservationDto.IsValid())
            {
                return BadRequest(new { message = "Invalid reservation data" });
            }

            await _reservationService.CreateReservation(reservationDto);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await _reservationService.GetAllReservations();
        return Ok(reservations);
    }

    [HttpGet("available-times")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAvailableTimes([FromQuery] Guid employeeId, [FromQuery] DateTime date)
    {
        if (employeeId == Guid.Empty)
        {
            return BadRequest(new { message = "Employee ID is required" });
        }

        if (date.Date < DateTime.UtcNow.Date)
        {
            return BadRequest(new { message = "Cannot check availability for past dates" });
        }

        var times = await _reservationService.GetAvailableTimesForEmployee(employeeId, date);
        return Ok(new
        {
            employeeId,
            date = date.Date,
            availableSlots = times
        });
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUpcomingReservations(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var start = startDate ?? DateTime.UtcNow.Date;
        var end = endDate ?? start.AddDays(7);

        if (end < start)
        {
            return BadRequest(new { message = "End date must be after start date" });
        }

        var reservations = await _reservationService.GetUpcomingReservations(start, end);
        return Ok(new
        {
            startDate = start,
            endDate = end,
            reservations = reservations
        });
    }

    [HttpPost("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelReservation(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { message = "Invalid reservation ID" });
        }

        var result = await _reservationService.CancelReservation(id);
        if (!result)
        {
            return NotFound(new { message = "Reservation not found" });
        }
        
        return NoContent();
    }

    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] AppointmentStatus status)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { message = "Invalid reservation ID" });
        }

        if (!Enum.IsDefined(typeof(AppointmentStatus), status))
        {
            return BadRequest(new { message = "Invalid appointment status" });
        }

        var result = await _reservationService.UpdateReservationStatus(id, status);
        if (!result)
        {
            return NotFound(new { message = "Reservation not found" });
        }
        
        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { message = "Invalid reservation ID" });
        }

        var reservation = await _reservationService.GetReservationById(id);
        if (reservation == null)
        {
            return NotFound(new { message = "Reservation not found" });
        }

        return Ok(reservation);
    }
}