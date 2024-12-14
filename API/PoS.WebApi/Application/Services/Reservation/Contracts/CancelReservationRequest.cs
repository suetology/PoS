using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Reservation.Contracts;

public class CancelReservationRequest
{
    [JsonIgnore]
    public Guid ReservationId { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
}