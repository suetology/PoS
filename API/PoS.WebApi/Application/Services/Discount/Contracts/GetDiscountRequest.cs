﻿using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Discount.Contracts;

public class GetDiscountRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}