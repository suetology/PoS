namespace PoS.WebApi.Application.Services.Item.Contracts;
using Domain.Entities;

public class ItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Guid? ItemGroupId { get; set; }

    //public ICollection<ItemTax>? ItemTaxes { get; set; }

    public Item ToDomain()
    {
        return new Item()
        {
            Name = Name,
            Description = Description,
            Price = Price,
            Stock = Stock,
            ItemGroupId = ItemGroupId,
            Image = new byte[0]
            //ItemTaxes = ItemTaxes
        };
    }
}
