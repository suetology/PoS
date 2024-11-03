using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Reservation;
using PoS.WebApi.Application.Services.Reservation.Contracts;

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
    public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    {
        await _reservationService.CreateReservation(reservationDto);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await _reservationService.GetAllReservations();

        return Ok(reservations);
    }
}
