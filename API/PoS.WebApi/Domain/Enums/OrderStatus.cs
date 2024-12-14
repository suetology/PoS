namespace PoS.WebApi.Domain.Enums;

public enum OrderStatus
{
    Open = 1,
    Closed,
    Paid,
    PartiallyPaid,
    Canceled,
    Refunded
}