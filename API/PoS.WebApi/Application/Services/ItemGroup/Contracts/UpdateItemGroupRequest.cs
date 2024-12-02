using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

public class UpdateItemGroupRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
}