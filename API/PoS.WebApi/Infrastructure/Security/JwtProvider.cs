using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Security.Configuration;
using PoS.WebApi.Infrastructure.Security.Exceptions;

namespace PoS.WebApi.Infrastructure.Security;

public class JwtProvider : IJwtProvider
{
    public static readonly IList<string> RevokedAccessTokenIds = new List<string>();
    
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly RSA _privateKey;

    public JwtProvider(
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        JwtConfiguration jwtConfiguration)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
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

    public async Task<string> GenerateRefreshToken(string accessToken)
    {
        var expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.RefreshToken.ExpiresInMinutes);

        var refreshToken = new RefreshToken
        {
            AccessToken = accessToken,
            Expires = expires,
        };
        
        await _refreshTokenRepository.Create(refreshToken);
        await _unitOfWork.SaveChanges();
        
        var serializedToken = JsonSerializer.Serialize(refreshToken);
        var encryptedToken = EncryptToken(serializedToken);

        return encryptedToken;
    }

    public async Task<(string, string)> RefreshAccessToken(string refreshToken)
    {
        var decryptedRefreshToken = DecryptToken(refreshToken);
        var deserializedToken = JsonSerializer.Deserialize<RefreshToken>(decryptedRefreshToken);

        if (deserializedToken == null)
        {
            throw new InvalidRefreshTokenException("Invalid refresh token");
        }
        
        var tokenInDb = await _refreshTokenRepository.GetByAccessToken(deserializedToken.AccessToken);

        if (tokenInDb == null)
        {
            throw new InvalidRefreshTokenException("Invalid refresh token");
        }

        if (tokenInDb.Expires < DateTime.UtcNow)
        {
            throw new ExpiredRefreshTokenException("Refresh token is expired");
        }
        
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(deserializedToken.AccessToken);
        var newAccessToken = await GenerateAccessToken(jwtToken.Claims);
        
        tokenInDb.AccessToken = newAccessToken;
        await _refreshTokenRepository.Update(tokenInDb);
        await _unitOfWork.SaveChanges();
        
        var serializedRefreshToken = JsonSerializer.Serialize(tokenInDb);
        var encryptedToken = EncryptToken(serializedRefreshToken);
        
        return (encryptedToken, newAccessToken);
    }

    public async Task RevokeTokens(string refreshToken)
    {
        var decryptedRefreshToken = DecryptToken(refreshToken);
        var deserializedToken = JsonSerializer.Deserialize<RefreshToken>(decryptedRefreshToken);

        if (deserializedToken == null)
        {
            throw new InvalidRefreshTokenException("Invalid refresh token");
        }
        
        var tokenInDb = await _refreshTokenRepository.GetByAccessToken(deserializedToken.AccessToken);

        if (tokenInDb == null)
        {
            throw new InvalidRefreshTokenException("Invalid refresh token");
        }

        if (tokenInDb.Expires < DateTime.UtcNow)
        {
            throw new ExpiredRefreshTokenException("Refresh token is expired");
        }
        
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(deserializedToken.AccessToken);
        var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

        if (jti != null)
        {
            RevokedAccessTokenIds.Add(jti.Value);
        }

        await _refreshTokenRepository.Delete(tokenInDb);
        await _unitOfWork.SaveChanges();
    }

    // Siaip reiketu cia normalesni encryptinga padaryt, bet dabar sueis
    private static string EncryptToken(string token)
    {
        var bytes = Encoding.UTF8.GetBytes(token);
        
        return Convert.ToBase64String(bytes);
    }

    private static string DecryptToken(string encryptedToken)
    {
        var bytes = Convert.FromBase64String(encryptedToken);
        
        return Encoding.UTF8.GetString(bytes);
    }
}