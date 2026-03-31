using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using QCEServices.Application.Authentication.Settings;
using QCEServices.Domain.Authentication;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Application.Authentication;

public sealed class TokenProvider(IOptions<AccessTokenSetting> accessTokenSetting, IOptions<RefreshTokenSetting> refreshTokenSetting, 
    IStringHasher stringHasher, ILogger<TokenProvider> logger) : ITokenProvider
{
    public AccessToken CreateAccessToken(User user)
    {
        try
        {
            var expires = DateTime.UtcNow.AddMinutes(accessTokenSetting.Value.ExpirationMinutes);
        
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSetting.Value.Secret));

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
                Issuer = accessTokenSetting.Value.Issuer,
                Audience = accessTokenSetting.Value.Audience,
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new AccessToken { Value = token, Expiration = expires };
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating access token for user. {ex.Message}");
            throw;
        }
    }

    public RefreshToken CreateRefreshToken(User user)
    {
        try
        {
            var value = $"{user.Id}{user.Username}{user.Email}{Guid.NewGuid()}";
            var token = stringHasher.HashValue(value, refreshTokenSetting.Value.Secret);

            return new RefreshToken { Value = token, Expiration = DateTime.UtcNow.AddDays(refreshTokenSetting.Value.ExpirationDays) };
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating refresh token for user. {ex.Message}");
            throw;
        }
    }
}