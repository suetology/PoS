﻿namespace PoS.WebApi.Application.Services.Customer.Contracts;
using Domain.Entities;
using Domain.Enums;

public class CustomerDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
