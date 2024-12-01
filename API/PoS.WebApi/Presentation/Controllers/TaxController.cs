using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Tax;
using PoS.WebApi.Application.Services.Tax.Contracts;

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

    [HttpGet]
    public async Task<IActionResult> GetAllTax()
    {
        var taxes = await _taxService.GetAllTaxes();

        return Ok(taxes);
    }

    [HttpGet("{taxId}", Name = nameof(GetTax))]
    public async Task<IActionResult> GetTax([FromRoute] Guid taxId)
    {
        var tax = await _taxService.GetTax(taxId);
        
        return Ok(tax);
    }

    [HttpPost(Name = nameof(CreateTax))]
    public async Task<IActionResult> CreateTax([FromBody] CreateTaxRequest request)
    {
        await _taxService.CreateTax(request);

        return NoContent();
    }
}