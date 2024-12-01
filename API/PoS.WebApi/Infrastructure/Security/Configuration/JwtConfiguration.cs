namespace PoS.WebApi.Infrastructure.Security.Configuration;

public class JwtConfiguration
{
    public string Issuer { get; set; }
    
    public string PublicKey { get; set; }
    
    public string PrivateKey { get; set; }
    
    public AccessTokenConfiguration AccessToken { get; set; }
    
    public RefreshTokenConfiguration RefreshToken { get; set; }
}