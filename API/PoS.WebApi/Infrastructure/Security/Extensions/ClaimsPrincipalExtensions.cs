using System.Security.Claims;
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