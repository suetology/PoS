using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Tax;
using PoS.WebApi.Application.Services.Tax.Contracts;
using PoS.WebApi.Domain.Entities;

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

    [HttpGet("{taxId}", Name = nameof(GetTax))]
    public async Task<IActionResult> GetTax([FromRoute] Guid taxId)
    {
        var tax = await _taxService.GetTax(taxId);
        
        return Ok(tax);
    }

    [HttpPost(Name = nameof(CreateTax))]
    public async Task<IActionResult> CreateTax([FromBody] TaxDto taxDto)
    {
        await _taxService.CreateTax(taxDto);

        return NoContent();
    }
}