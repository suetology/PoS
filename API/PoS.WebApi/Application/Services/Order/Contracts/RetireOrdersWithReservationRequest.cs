using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class RetireOrdersWithReservationRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    public Guid ServiceId { get; set; }
}