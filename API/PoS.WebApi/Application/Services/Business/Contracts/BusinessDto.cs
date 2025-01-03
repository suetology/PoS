﻿namespace PoS.WebApi.Application.Services.Business.Contracts;

using Domain.Entities;

public class BusinessDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
}