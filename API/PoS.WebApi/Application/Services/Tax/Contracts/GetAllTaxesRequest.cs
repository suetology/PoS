using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Tax.Contracts;

public class GetAllTaxesRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}