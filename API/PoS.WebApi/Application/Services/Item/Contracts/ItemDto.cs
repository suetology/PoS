namespace PoS.WebApi.Application.Services.Item.Contracts;
using Domain.Entities;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using PoS.WebApi.Application.Services.Tax.Contracts;

public class ItemDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int Stock { get; set; }
    
    public byte[] Image { get; set; }
    
    public Guid? ItemGroupId { get; set; }

    public ItemGroupDto ItemGroup { get; set; }

    public List<Guid> TaxIds { get; set; } = new List<Guid>();

    public List<TaxDto> Taxes { get; set; } = new List<TaxDto>();

    public List<DiscountDto> Discounts { get; set; } = new List<DiscountDto>();
}
