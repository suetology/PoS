namespace PoS.WebApi.Application.Services.Reservation;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Application.Services.Reservation.Contracts;

public interface IReservationService
{
    Task CreateReservation(ReservationDto reservationDto);
    Task<IEnumerable<Reservation>> GetAllReservations();
}
