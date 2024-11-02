using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Business;
using PoS.WebApi.Application.Services.Business.Contracts;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("business")]
public class BusinessController : ControllerBase
{
    private readonly IBusinessService _businessService;

    public BusinessController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var allBusinesses = await _businessService.GetAllBusiness();

        return Ok(allBusinesses);
    }

    [HttpGet("{businessId}", Name = nameof(GetBusiness))]
    public async Task<IActionResult> GetBusiness([FromRoute] Guid businessId)
    {
        var business = await _businessService.GetBusiness(businessId);

        return Ok(business);
    }

    [HttpPost(Name = nameof(CreateBusiness))]
    public async Task<IActionResult> CreateBusiness([FromBody] BusinessDto businessDto)
    {
        await _businessService.CreateBusiness(businessDto);

        return NoContent();
    }

    [HttpPatch("{businessId}", Name = nameof(UpdateBusiness))]
    public async Task<IActionResult> UpdateBusiness([FromRoute] Guid businessId, [FromBody] BusinessDto businessDto)
    {
        var sucess = await _businessService.UpdateBusiness(businessId, businessDto);

        if (!sucess)
        {
            return NotFound();
        }

        return NoContent();
    }
}
