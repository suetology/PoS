using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Discount.Contracts;

public class CreateDiscountRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }

    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;

    public int AmountAvailable { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public List<Guid> ApplicableItems { get; set; }
    
    public List<Guid> ApplicableGroups { get; set; }
}