
namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Domain.Enums;

public enum OrderSortableFields {
    OrderId,
    EmployeeId,
    Status,
    OrderCreated,
    OrderClosed,
    DiscountId,
    DiscountAmount,
    ServiceChargeID,
    ServiceChargeAmount,
    TipAmount,
    FinalAmount,
    PaidAmount
}

public class OrderQueryParameters
{
    public OrderStatus? Status { get; set; }

    public Guid? EmployeeId { get; set; }

    public OrderSortableFields OrderBy { get; set; } = OrderSortableFields.OrderId;

    public SortOrder SortOrder { get; set; } = SortOrder.Descending;

    public int Page { get; set; } = 1;

    const int MaxPageSize = 100;

    private int _pageSize = 20;

    public int PageSize 
    {
        get { return _pageSize; }
        set { _pageSize = Math.Clamp(value, 1, MaxPageSize); }
    }
}