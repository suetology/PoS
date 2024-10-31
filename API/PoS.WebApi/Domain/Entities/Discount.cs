namespace PoS.WebApi.Domain.Entities;

public class Discount : Entity
{
    public string Name { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;
    
    public int AmountAvailable { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; }
    
    public ICollection<ItemDiscount> ItemDiscounts { get; set; } // abejoju ar reik
    
    public ICollection<GroupDiscount> GroupDiscounts { get; set; } // irgi abejoju ar reik
}