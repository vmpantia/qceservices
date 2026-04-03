using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Application.Authentication;

public sealed class TokenProvider(IOptions<JwtSetting> jwtSetting, ILogger<TokenProvider> logger) : ITokenProvider
{
    public string Create(User user)
    {
        try
        {
            var expires = DateTime.UtcNow.AddMinutes(jwtSetting.Value.ExpirationInMinutes);
        
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Value.Secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Upn, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.Name.LastName}, {user.Name.FirstName}"),
                    new Claim(ClaimTypes.Email, user.Email)
                ]),
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = jwtSetting.Value.Issuer,
                Audience = jwtSetting.Value.Audience,
            };

            var handler = new JsonWebTokenHandler();
             return handler.CreateToken(tokenDescriptor);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating access token for user. {ex.Message}");
            throw;
        }
    }
}