using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class Payment : Entity
{
    public PaymentMethod Method { get; set; }
    
    public decimal Amount { get; set; }
    
    public string StripeTransactionId { get; set; }
    
    public DateTime Date { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}