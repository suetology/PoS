﻿using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Business.Contracts;

public class UpdateBusinessRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
}