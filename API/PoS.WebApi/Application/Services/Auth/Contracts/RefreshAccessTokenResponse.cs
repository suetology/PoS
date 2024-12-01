namespace PoS.WebApi.Application.Services.Auth.Contracts;

public class RefreshAccessTokenResponse
{
    public string RefreshToken { get; set; }
    
    public string AccessToken { get; set; }
}