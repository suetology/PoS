using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

public class GetItemGroupRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}