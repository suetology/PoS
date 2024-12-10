using PoS.WebApi.Application.Services.Item.Contracts;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class OrderItemDto 
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public ItemDto Item { get; set; }

    public IEnumerable<ItemVariationDto> ItemVariations { get; set; }
}