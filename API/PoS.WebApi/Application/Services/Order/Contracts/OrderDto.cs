
namespace PoS.WebApi.Application.Services.Order.Contracts;

using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Domain.Entities;

public class OrderDto 
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }

    public OrderStatus Status { get; set; }

    public Guid? DiscountId { get; set; }

    public decimal DiscountAmount { get; set; }

    public Guid? ServiceChargeId { get; set; }

    public decimal ServiceChargeAmount { get; set; }

    public decimal TipAmount { get; set; }

    public decimal FinalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime Created { get; set; }

    public DateTime Closed { get; set; }
}