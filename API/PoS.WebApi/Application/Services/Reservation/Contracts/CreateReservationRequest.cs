using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class CreateReservationRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    [JsonIgnore]
    public Guid OrderId { get; set; }
    
    public DateTime AppointmentTime { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public Guid CustomerId { get; set; }
}