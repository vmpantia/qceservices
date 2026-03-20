using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Application.Common.Authentication;

public sealed class TokenProvider(JwtSetting jwtSetting, ILogger<TokenProvider> logger) : ITokenProvider
{
    public string Create(User user)
    {
        try
        {
            var expires = DateTime.UtcNow.AddMinutes(jwtSetting.ExpirationInMinutes);
        
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret));

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
                Issuer = jwtSetting.Issuer,
                Audience = jwtSetting.Audience,
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return token;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating authentication token for user. {ex.Message}");
            throw;
        }
    }
}