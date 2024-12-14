using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class CancelOrderRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
}