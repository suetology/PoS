using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Item.Contracts;

public class CreateItemVariationRequest
{
    [JsonIgnore]
    public Guid ItemId { get; set; }

    [JsonIgnore] 
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal AddedPrice { get; set; }
    
    public int Stock { get; set; }
}