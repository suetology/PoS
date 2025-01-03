﻿using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Customer : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public bool Retired { get; set; } = false;
    
    public ICollection<Order> Orders { get; set; }
}