namespace PoS.WebApi.Application.Services.Auth.Contracts;

public class LogoutRequest
{
    public string RefreshToken { get; set; }
}