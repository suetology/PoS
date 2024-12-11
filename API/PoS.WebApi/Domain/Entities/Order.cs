using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class Order : Entity
{
    public Guid BusinessId { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime? Closed { get; set; }

    //public decimal FinalAmount { get; set; }  // neturi buti, galima paskaiciuot is OrderItem saraso

    //public decimal PaidAmount { get; set; } // same neturi buti, nes is Paymentu skaiciuojasi
    
    public decimal TipAmount { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    
    public Refund Refund { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public User Employee { get; set; }
    
    public Guid? ServiceChargeId { get; set; }
    
    public ServiceCharge ServiceCharge { get; set; }
    
    //public decimal ServiceChargeAmount { get; set; } // ar tikrai reik jei yra ServiceCharge cia?
    
    public Guid? DiscountId { get; set; }
    
    public Discount Discount { get; set; }
    
    //public decimal DiscountAmount { get; set; } // vel gi ar reik jei Discount yra?
    
    public Reservation Reservation { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }

    public decimal CalculateTotalAmout()
    {
        // add discounts
        var totalAmount = OrderItems.Sum(o => o.CalculateTotalAmout()) + (Reservation?.Service.Price ?? 0);
        totalAmount += ServiceCharge == null ? 0 : (ServiceCharge.IsPercentage ? totalAmount * (ServiceCharge.Value / 100) : ServiceCharge.Value);
        totalAmount += TipAmount;

        return totalAmount;
    }

    public decimal CalculatePaidAmount()
    {
        return Payments.Sum(p => p.Amount);
    }
}