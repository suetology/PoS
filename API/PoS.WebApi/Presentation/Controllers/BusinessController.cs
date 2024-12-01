using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "SuperAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var response = await _businessService.GetAllBusiness();

        return Ok(response);
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpGet("{businessId}", Name = nameof(GetBusiness))]
    public async Task<IActionResult> GetBusiness([FromRoute] Guid businessId)
    {
        var response = await _businessService.GetBusiness(businessId);

        return Ok(response);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost(Name = nameof(CreateBusiness))]
    public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request)
    {
        await _businessService.CreateBusiness(request);

        return NoContent();
    }

    [Authorize(Roles = "SuperAdmin,BusinessOwner")]
    [HttpPatch("{businessId}", Name = nameof(UpdateBusiness))]
    public async Task<IActionResult> UpdateBusiness([FromRoute] Guid businessId, [FromBody] UpdateBusinessRequest request)
    {
        var sucess = await _businessService.UpdateBusiness(businessId, request);

        if (!sucess)
        {
            return NotFound();
        }

        return NoContent();
    }
}
