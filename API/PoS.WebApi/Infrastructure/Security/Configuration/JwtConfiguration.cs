namespace PoS.WebApi.Infrastructure.Security.Configuration;

public class JwtConfiguration
{
    public string Issuer { get; set; }
    
    public string PublicKey { get; set; }
    
    public string PrivateKey { get; set; }
    
    public AccessToken AccessToken { get; set; }
}