using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Reservation;

public class GetAllReservationsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}