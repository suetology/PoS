﻿using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Item.Contracts;

public class CreateItemRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; } 
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int Stock { get; set; }
    
    public string Image { get; set; }
    
    public Guid? ItemGroupId { get; set; }

    public List<Guid> TaxIds { get; set; } = new List<Guid>();
}