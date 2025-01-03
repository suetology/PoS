using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Customer.Contracts;
using PoS.WebApi.Application.Services.Order;
using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Application.Services.Service;
using PoS.WebApi.Application.Services.Service.Contracts;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IServiceService _serviceService;
    private readonly IServiceRepository _serviceRepository;
    private readonly IOrderService _orderService;

    public UserController(IUserService userService, IServiceService serviceService, IServiceRepository serviceRepository, IOrderService orderService)
    {
        _userService = userService;
        _serviceService = serviceService;
        _serviceRepository = serviceRepository;
        _orderService = orderService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpPost(Name = nameof(CreateUser))]
    [Tags("User Management")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        await _userService.CreateUser(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("User Management")]
    [ProducesResponseType(typeof(GetAllUsersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUsers([FromQuery] QueryParameters parameters)
    {
        if (!QueryParameters.AllowedSortFields.Contains(parameters.OrderBy.ToLower()))
        {
            return BadRequest("Invalid sorting field. Allowed fields are name, surname, username, email, dateOfEmployment, and role.");
        }
        
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllUsersRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };

        var response = await _userService.GetAllUsers(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet("active")]
    [Tags("User Management")]
    [ProducesResponseType(typeof(GetAllCustomersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllActiveUsers([FromQuery] QueryParameters parameters)
    {
        if (!QueryParameters.AllowedSortFields.Contains(parameters.OrderBy.ToLower()))
        {
            return BadRequest("Invalid sorting field. Allowed fields are name, surname, username, email, dateOfEmployment, and role.");
        }

        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetAllUsersRequest
        {
            BusinessId = businessId.Value,
            QueryParameters = parameters
        };
        
        var response = await _userService.GetAllActiveUsers(request);

        return Ok(response);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpGet]
    [Tags("User Management")]
    [Route("{userId}", Name = nameof(GetUser))]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        var request = new GetUserRequest
        {
            Id = userId,
            BusinessId = businessId.Value
        };
        
        var response = await _userService.GetUser(request);

        return Ok(response);
    }


    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [Tags("User Management")]
    [HttpPatch("{userId}", Name = nameof(UpdateUser))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        request.Id = userId;

        await _userService.UpdateUser(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)}")]
    [HttpGet("roles", Name = nameof(GetAvailableRoles))]
    [Tags("User Management")]
    [ProducesResponseType(typeof(GetAvailableRolesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAvailableRoles()
    {
        var roles = await _userService.GetAvailableRoles();

        return Ok(roles);
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)}")]
    [HttpPatch("{userId}/set-business", Name = nameof(SetBusiness))]
    [Tags("User Management")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetBusiness([FromRoute] Guid userId, [FromBody] SetBusinessRequest request)
    {
        request.UserId = userId;

        await _userService.SetBusiness(request);

        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]
    [HttpPatch("{userId}/retire")]
    [Tags("User Management")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RetireUser(Guid userId, RetireUserRequest request)
    {
        var businessId = User.GetBusinessId();
        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.Id = userId;
        request.BusinessId = businessId.Value;
        
        await _userService.RetireUser(request);

        var services = await _serviceRepository.GetAll();
        var serviceDtos = services
            .Where(s => s.BusinessId == request.BusinessId && true == s.IsActive && s.EmployeeId == userId)
            .Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Duration = s.Duration,
                IsActive = s.IsActive,
                EmployeeId = s.EmployeeId
            });

        foreach (var service in serviceDtos) {
            if(service.EmployeeId == userId) {
                var retireServiceRequest = new RetireServiceRequest {
                    Id = service.Id,
                    BusinessId = request.BusinessId
                };

                await _serviceService.RetireService(retireServiceRequest);

                var retireOrdersWithReservationRequest = new RetireOrdersWithReservationRequest
                {
                    BusinessId = request.BusinessId,
                    ServiceId = service.Id
                };

                await _orderService.RetireOrdersWithReservation(retireOrdersWithReservationRequest);                
            }
        }

        return NoContent();
    }
}
