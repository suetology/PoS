
namespace PoS.WebApi.Application.Services.Order.Contracts;

using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Domain.Entities;

public class OrderDto {

    public Guid EmployeeId { get; set; }

    public OrderStatus Status { get; set; }

    public Guid? DiscountId { get; set; }

    public decimal DiscountAmount { get; set; }

    public Guid? ServiceChargeId { get; set; }

    public decimal ServiceChargeAmount { get; set; }

    public decimal TipAmount { get; set; }

    public decimal FinalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime OrderCreated { get; }

    public DateTime OrderClosed { get; }

    public Order ToDomain()
    {
        return new Order
        {
            Status = Status,
            Created = OrderCreated,
            Closed = OrderClosed,
            FinalAmount = FinalAmount,
            PaidAmount = PaidAmount,
            TipAmount = TipAmount,
            EmployeeId = EmployeeId,
            ServiceChargeId = ServiceChargeId,
            ServiceChargeAmount = ServiceChargeAmount,
            DiscountId = DiscountId,
            DiscountAmount = DiscountAmount
        };
    }
}