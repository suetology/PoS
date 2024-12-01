using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PoS.WebApi.Infrastructure.Security;
using PoS.WebApi.Infrastructure.Security.Configuration;

namespace PoS.WebApi.Presentation.Extensions;

public static class AuthenticationExtensions
{
    public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var jwtConfigurationSection = builder.Configuration.GetSection("Jwt");
        var jwtConfiguration = jwtConfigurationSection.Get<JwtConfiguration>();
        
        var publicKey = RSA.Create();
        publicKey.ImportFromPem(jwtConfiguration.PublicKey);
        
        builder.Services.Configure<JwtConfiguration>(jwtConfigurationSection);

        builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<JwtConfiguration>>().Value);
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(publicKey)
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var tokenId = context.Principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

                        if (JwtProvider.RevokedAccessTokenIds.Contains(tokenId))
                        {
                            context.Fail("Token has been revoked");
                        }
                    }
                };
            });

        return builder;
    }
}