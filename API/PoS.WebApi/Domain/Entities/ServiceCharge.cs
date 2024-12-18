using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class ServiceCharge : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;

    public bool Retired { get; set; } = false;
    
    public DateTime LastUpdated { get; set; }
}