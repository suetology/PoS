using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Item : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public byte[] Image { get; set; }
    
    public decimal Price { get; set; } // doke buvo parasyta kad int yra, bet manau klaida
    
    public int Stock { get; set; }
    
    public Guid? ItemGroupId { get; set; }
    
    public ItemGroup? ItemGroup { get; set; }
    
    public ICollection<Tax> Taxes { get; set; } = new List<Tax>();

    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public ICollection<ItemVariation> ItemVariations { get; set; } = new List<ItemVariation>();

    public decimal CalculateTotalAmount()
    {
        var totalAmount = Price + Taxes.Sum(t => t.IsPercentage ? Price * (t.Value / 100) : t.Value);
        var percentageDiscountAmount = Discounts.Sum(d => d.IsPercentage ? d.Value : 0) + ItemGroup?.Discounts?.Sum(d => d.IsPercentage ? d.Value : 0);
        var nonPercentageDiscountsAmount = Discounts.Sum(d => d.IsPercentage ? 0 : d.Value) + ItemGroup?.Discounts?.Sum(d => d.IsPercentage ? 0 : d.Value);

        totalAmount = totalAmount * (1 - (percentageDiscountAmount ?? 0)/ 100) - (nonPercentageDiscountsAmount ?? 0);

        return Math.Max(0, totalAmount);
    }
}