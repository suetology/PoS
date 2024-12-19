using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Tax.Contracts;

public class RetireTaxRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
}