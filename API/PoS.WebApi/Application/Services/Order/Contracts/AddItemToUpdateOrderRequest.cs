using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class AddItemToUpdateOrderRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public List<Guid> ItemVariationsIds { get; set; } = new();
}
