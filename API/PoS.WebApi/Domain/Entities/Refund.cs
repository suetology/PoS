﻿using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Refund : Entity
{
    public Guid BusinessId { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Reason { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}