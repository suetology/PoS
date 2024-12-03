using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Infrastructure.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public const string RoleClaimName = "role";
    public const string BusinessIdClaimName = "business_id";
    
    public static Guid? GetBusinessId(this ClaimsPrincipal user)
    {
        var businessIdClaim = user?.Claims.FirstOrDefault(c => c.Type == BusinessIdClaimName);

        if (businessIdClaim == null || !Guid.TryParse(businessIdClaim.Value, out var businessId))
        {
            return null;
        }

        return businessId;
    }

    public static Guid? GetEmployeeId(this ClaimsPrincipal user)
    {
        var employeeIdClaim = user?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

        if (employeeIdClaim == null || !Guid.TryParse(employeeIdClaim.Value, out var employeeId))
        {
            return null;
        }

        return employeeId;
    }

    public static Role? GetRole(this ClaimsPrincipal user)
    {
        var roleClaim = user?.Claims.FirstOrDefault(c => c.Type == RoleClaimName);

        if (roleClaim == null || !Enum.TryParse<Role>(roleClaim.Value, out var role))
        {
            return null;
        }

        return role;
    }
}