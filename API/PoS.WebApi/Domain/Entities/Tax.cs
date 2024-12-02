using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class Tax : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public TaxType Type { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;
    
    public DateTime LastUpdated { get; set; }
}