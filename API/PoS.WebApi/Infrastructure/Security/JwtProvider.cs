using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Security.Configuration;

namespace PoS.WebApi.Infrastructure.Security;

public class JwtProvider : IJwtProvider
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly RSA _privateKey;

    public JwtProvider(JwtConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
        
        _privateKey = RSA.Create();
        _privateKey.ImportFromPem(jwtConfiguration.PrivateKey);
    }
    
    public Task<string> GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessToken.ExpiresInMinutes);
        
        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(_privateKey),
            SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            expires: expires,
            claims: claims,
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        
        return Task.FromResult(tokenHandler.WriteToken(token));
    }

    public Task<RefreshToken> GenerateRefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}