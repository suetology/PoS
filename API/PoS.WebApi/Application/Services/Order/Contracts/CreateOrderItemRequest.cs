namespace PoS.WebApi.Application.Services.Order.Contracts;

public class CreateOrderItemRequest
{
    public Guid ItemId { get; set; }
    
    public IEnumerable<Guid> ItemVariationsIds { get; set; }

    public int Quantity { get; set; }
}