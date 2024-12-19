using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Business;
using PoS.WebApi.Application.Services.Business.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

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

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)}")]
    [HttpGet]
    [ProducesResponseType(typeof(GetAllBusinessesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var response = await _businessService.GetAllBusiness();

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("{businessId}", Name = nameof(GetBusiness))]
    [ProducesResponseType(typeof(GetBusinessResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetBusiness([FromRoute] Guid businessId)
    {
        if (User.GetBusinessId() != businessId)
        {
            return Unauthorized("Can't access other business data");
        }

        var request = new GetBusinessRequest
        {
            Id = businessId
        };
        
        var response = await _businessService.GetBusiness(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)}")]
    [HttpPost(Name = nameof(CreateBusiness))]
    [ProducesResponseType(typeof(BusinessDto),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request)
    {
        await _businessService.CreateBusiness(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPatch("{businessId}", Name = nameof(UpdateBusiness))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBusiness([FromRoute] Guid businessId, [FromBody] UpdateBusinessRequest request)
    {
        var businessIdClaim = User.GetBusinessId();
        
        if (businessIdClaim != businessId)
        {
            return Unauthorized("Can't access other business data");
        }

        request.Id = businessId;
        
        await _businessService.UpdateBusiness(request);

        return NoContent();
    }
}
