﻿namespace PoS.WebApi.Application.Services.Service.Contracts;

public class GetServicesResponse
{
    public IEnumerable<ServiceDto> Services { get; set; }
}