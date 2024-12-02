using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Reservation;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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

    [HttpPost(Name = nameof(CreateReservation))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _reservationService.CreateReservation(request);
        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllReservations()
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllReservationsRequest
        {
            BusinessId = businessId.Value
        };
        
        var response = await _reservationService.GetAllReservations(request);
        return Ok(response);
    }
    
    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { message = "Invalid reservation ID" });
        }
        
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetReservationRequest
        {
            Id = id,
            BusinessId = businessId.Value
        };

        var response = await _reservationService.GetReservationById(request);
        if (response == null)
        {
            return NotFound(new { message = "Reservation not found" });
        }

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateReservation(Guid id, UpdateReservationRequest request)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { message = "Invalid reservation ID" });
        }
        
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.Id = id;
        request.BusinessId = businessId.Value;

        await _reservationService.UpdateReservation(request);
        
        return NoContent();
    }

/*    [HttpGet("available-times")]
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
    }*/
}