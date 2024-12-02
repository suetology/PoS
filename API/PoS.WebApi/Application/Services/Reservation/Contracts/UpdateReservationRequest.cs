using System.Text.Json.Serialization;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class UpdateReservationRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public AppointmentStatus? Status { get; set; }
    
    public DateTime? AppointmentTime { get; set; }
}