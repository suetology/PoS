using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Tax;
using PoS.WebApi.Application.Services.Tax.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("tax")]
public class TaxController : ControllerBase
{
    private readonly ITaxService _taxService;

    public TaxController(ITaxService taxService)
    {
        _taxService = taxService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    public async Task<IActionResult> GetAllTax()
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllTaxesRequest
        {
            BusinessId = businessId.Value
        };
        
        var taxes = await _taxService.GetAllTaxes(request);

        return Ok(taxes);
    }
    
    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("{taxId}", Name = nameof(GetTax))]
    public async Task<IActionResult> GetTax([FromRoute] Guid taxId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetTaxRequest
        {
            Id = taxId,
            BusinessId = businessId.Value
        };
        
        var tax = await _taxService.GetTax(request);
        
        return Ok(tax);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost(Name = nameof(CreateTax))]
    public async Task<IActionResult> CreateTax([FromBody] CreateTaxRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _taxService.CreateTax(request);

        return NoContent();
    }
}