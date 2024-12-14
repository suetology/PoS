using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class AddTipRequest
{
    [JsonIgnore]
    public Guid OrderId { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }

    public decimal TipAmount { get; set; }
}