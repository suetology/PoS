using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Item.Contracts;

public class GetItemVariationRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid ItemId { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}