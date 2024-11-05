namespace PoS.WebApi.Application.Services.Auth.Contracts;

public class LoginRequest
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}