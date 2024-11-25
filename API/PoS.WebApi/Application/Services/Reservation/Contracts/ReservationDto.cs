namespace PoS.WebApi.Application.Services.Reservation.Contracts;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;

public class ReservationDto
{
    public DateTime AppointmentTime { get; set; }
    public Guid CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid OrderId { get; set; } = Guid.Empty;
    
    // Added validation
    public bool IsValid()
    {
        return AppointmentTime > DateTime.UtcNow &&
               CustomerId != Guid.Empty &&
               EmployeeId != Guid.Empty;
    }

    public Reservation ToDomain(Guid assignedOrderId)
    {
        if (!IsValid())
        {
            throw new InvalidOperationException("Invalid reservation data");
        }

        return new Reservation
        {
            ReservationTime = DateTime.UtcNow,
            AppointmentTime = AppointmentTime,
            Status = AppointmentStatus.Booked,
            NotificationSent = false,
            CustomerId = CustomerId,
            EmployeeId = EmployeeId,
            OrderId = assignedOrderId
        };
    }
}