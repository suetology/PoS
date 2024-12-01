namespace PoS.WebApi.Application.Services.Order.Contracts;

public class OrderItemDto
{
    public Guid ItemId { get; set; }
    
    public int Quantity { get; set; }
}