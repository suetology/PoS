using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Auth.Contracts;

public class LoginResponse
{
    public Guid UserId { get; set; }

    public Guid BusinessId { get; set; }

    public Role Role { get; set; }

    public string AccessToken { get; set; }
    
    public string RefreshToken { get; set; }
}