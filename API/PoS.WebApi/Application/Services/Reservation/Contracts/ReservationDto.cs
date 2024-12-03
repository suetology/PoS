namespace PoS.WebApi.Application.Services.Reservation.Contracts;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;

public class ReservationDto
{
    public Guid Id { get; set; }
    
    public bool NotificationSent { get; set; }
    
    public AppointmentStatus Status { get; set; }
    
    public DateTime ReservationTime { get; set; }
    
    public DateTime AppointmentTime { get; set; }

    public Guid CustomerId { get; set; }
    
    public Guid EmployeeId { get; set; }
}