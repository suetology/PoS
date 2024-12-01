using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.Order.Contracts;

public class CreateOrderRequest
{
    public Guid EmployeeId { get; set; }

    public Guid? DiscountId { get; set; }

    public Guid? ServiceChargeId { get; set; }
    
    public IEnumerable<OrderItem> OrderItems { get; set; }
}