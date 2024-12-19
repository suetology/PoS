using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Shift.Contracts;
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
    [ProducesResponseType(typeof(GetAllTaxesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [HttpGet("valid")]
    [ProducesResponseType(typeof(GetAllTaxesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllValidTax()
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
        
        var taxes = await _taxService.GetAllValidTaxes(request);

        return Ok(taxes);
    }
    
    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("{taxId}", Name = nameof(GetTax))]
    [ProducesResponseType(typeof(GetTaxResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPatch("{taxId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTax(Guid taxId, [FromBody] UpdateTaxRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.Id = taxId;
        request.BusinessId = businessId.Value;

        await _taxService.UpdateTax(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPatch("{taxId}/retire")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RetireTax(Guid taxId, RetireTaxRequest request)
    {
        var businessId = User.GetBusinessId();
        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.Id = taxId;
        request.BusinessId = businessId.Value;

        await _taxService.RetireTax(request);

        return NoContent();
    }
}