using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Tax.Contracts;

public class GetTaxRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}