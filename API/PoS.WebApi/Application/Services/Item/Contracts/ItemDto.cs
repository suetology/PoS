namespace PoS.WebApi.Application.Services.Item.Contracts;
using Domain.Entities;

public class ItemDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int Stock { get; set; }
    
    public byte[] Image { get; set; }
    
    public Guid? ItemGroupId { get; set; }

    public List<Guid> TaxIds { get; set; } = new List<Guid>();
}
