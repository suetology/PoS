using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class UpdateReservationRequest
{
    public AppointmentStatus? Status { get; set; }
    
    public DateTime? AppointmentTime { get; set; }
}