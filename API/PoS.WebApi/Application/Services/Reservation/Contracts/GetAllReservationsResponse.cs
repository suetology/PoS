namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class GetAllReservationsResponse
{
    public IEnumerable<ReservationDto> Reservations { get; set; }
}