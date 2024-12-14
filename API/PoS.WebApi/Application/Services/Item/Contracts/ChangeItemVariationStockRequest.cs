using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Item.Contracts;

public class ChangeItemVariationStockRequest
{
    [JsonIgnore]
    public Guid ItemVariationId { get; set; }

    [JsonIgnore]
    public Guid ItemId { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }

    [JsonIgnore]
    public int StockChange { get; set; }
}