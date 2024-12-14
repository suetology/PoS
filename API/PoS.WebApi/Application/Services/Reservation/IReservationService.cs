namespace PoS.WebApi.Application.Services.Reservation;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Application.Services.Reservation.Contracts;

public interface IReservationService
{
    Task CreateReservation(CreateReservationRequest request);
    Task<GetAllReservationsResponse> GetAllReservations(GetAllReservationsRequest request);
    Task<GetReservationResponse> GetReservationById(GetReservationRequest request);
    Task<bool> UpdateReservation(UpdateReservationRequest request);
    Task CancelReservation(CancelReservationRequest request);
}