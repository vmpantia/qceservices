using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Application.Authentication;

public sealed class TokenProvider(AuthenticationSetting authSetting, IStringHasher stringHasher, ILogger<TokenProvider> logger) : ITokenProvider
{
    public string CreateAccessToken(User user)
    {
        try
        {
            var expires = DateTime.UtcNow.AddMinutes(authSetting.Jwt.ExpirationInMinutes);
        
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.Jwt.Secret));

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
                Issuer = authSetting.Jwt.Issuer,
                Audience = authSetting.Jwt.Audience,
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return token;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating access token for user. {ex.Message}");
            throw;
        }
    }

    public Token CreateRefreshToken(User user)
    {
        try
        {
            var value = $"{user.Id}{user.Name.LastName}{user.Name.FirstName}{user.Email}{user.Password}";
            var token = stringHasher.HashValue(value, authSetting.TokenSecret); 

            return new Token
            {
                UserId = user.Id,
                Value = token,
                IsRevoked = false,
                ExpiresAt =  DateTime.UtcNow.AddDays(authSetting.TokenExpirationInDays)
            };
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating refresh token for user. {ex.Message}");
            throw;
        }
    }
}