namespace PoS.WebApi.Application.Services.Reservation;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Application.Services.Reservation.Contracts;

public interface IReservationService
{
    Task CreateReservation(ReservationDto reservationDto);
    Task<IEnumerable<Reservation>> GetAllReservations();
    Task<IEnumerable<DateTime>> GetAvailableTimesForEmployee(Guid employeeId, DateTime date);
    Task<bool> CancelReservation(Guid reservationId);
    Task<IEnumerable<Reservation>> GetUpcomingReservations(DateTime startDate, DateTime endDate);
    Task SendNotifications();
    Task<bool> UpdateReservationStatus(Guid reservationId, Domain.Enums.AppointmentStatus status);
    Task<Reservation> GetReservationById(Guid id);
}