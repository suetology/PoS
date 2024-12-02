using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

public class CreateServiceChargeRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Value { get; set; }
    
    public bool IsPercentage { get; set; }
}