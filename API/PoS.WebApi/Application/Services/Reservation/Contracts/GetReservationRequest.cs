using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class GetReservationRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}