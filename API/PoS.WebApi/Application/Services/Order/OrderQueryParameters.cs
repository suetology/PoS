
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

    public void SetSortableField(string field) {
        try {
            OrderBy = Enum.TryParse(field, true, out OrderSortableFields sortableField) switch {
                true => sortableField,
                false => OrderSortableFields.OrderId
            };
        }
        catch (ArgumentException) {
            OrderBy = OrderSortableFields.OrderId;
        }
        catch (InvalidOperationException) {
            OrderBy = OrderSortableFields.OrderId;
        }
    }

    public SortOrder SortOrder { get; set; } = SortOrder.Descending;

    public void SetSortOrder(string order) {
        try {
            SortOrder = Enum.TryParse(order, true, out SortOrder sortOrder) switch {
                true => sortOrder,
                false => SortOrder.Descending
            };
        }
        catch (ArgumentException) {
            SortOrder = SortOrder.Descending;
        }
        catch (InvalidOperationException) {
            SortOrder = SortOrder.Descending;
        }
    }

    public uint Page { get; set; } = 1;

    const uint MaxPageSize = 100;

    private uint _pageSize = 20;

    public uint PageSize {
        get { return _pageSize; }
        set { _pageSize = Math.Clamp(value, 1, MaxPageSize); }
    }
}