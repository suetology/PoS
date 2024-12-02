using System.Text.Json.Serialization;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Tax.Contracts;

public class CreateTaxRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; } 
    
    public string Name { get; set; }
    
    public TaxType Type { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;
}