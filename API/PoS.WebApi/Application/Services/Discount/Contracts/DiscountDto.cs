namespace PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Domain.Entities;
public class DiscountDto
{
    public string Name { get; set; }

    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;

    public int AmountAvailable { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public List<ItemDto> Items { get; set; }
    
    public List<ItemGroupDto> ItemGroups { get; set; }
}

