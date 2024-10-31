using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class Reservation : Entity
{
    public DateTime ReservationTime { get; set; }
    
    public DateTime AppointmentTime { get; set; }
    
    public AppointmentStatus Status { get; set; }
    
    public bool NotificationSent { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public User Employee { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}