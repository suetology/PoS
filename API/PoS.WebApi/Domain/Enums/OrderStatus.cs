namespace PoS.WebApi.Domain.Enums;

public enum OrderStatus
{
    Open = 1,
    Closed,
    PartiallyPaid,
    Canceled,
    Refunded
}