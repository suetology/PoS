namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class CreateReservationRequest
{
    public DateTime AppointmentTime { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public Guid OrderId { get; set; }
}