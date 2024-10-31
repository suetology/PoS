using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class Order : Entity
{
    public OrderStatus Status { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime Closed { get; set; }

    public decimal FinalAmount { get; set; }  // neturi buti, galima paskaiciuot is OrderItem saraso

    public decimal PaidAmount { get; set; } // same neturi buti, nes is Paymentu skaiciuojasi
    
    public decimal? TipAmount { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
    
    public ICollection<Payment> Payments { get; set; }
    
    public ICollection<OrderService> OrderServices { get; set; }
    
    public Refund? Refund { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public User Employee { get; set; }
    
    public Guid ServiceChargeId { get; set; }
    
    public ServiceCharge ServiceCharge { get; set; }
    
    public decimal ServiceChargeAmount { get; set; } // ar tikrai reik jei yra ServiceCharge cia?
    
    public Guid? DiscountId { get; set; }
    
    public Discount? Discount { get; set; }
    
    public decimal? DiscountAmount { get; set; } // vel gi ar reik jei Discount yra?
    
    public Reservation? Reservation { get; set; }
}