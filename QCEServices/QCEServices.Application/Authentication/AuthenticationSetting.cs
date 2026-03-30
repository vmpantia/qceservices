using Microsoft.Extensions.Configuration;

namespace QCEServices.Application.Authentication;

public sealed class AuthenticationSetting
{
    private AuthenticationSetting(string tokenSecret, int tokenExpirationInDays, JwtSetting jwt)
    {
        TokenSecret = tokenSecret;
        TokenExpirationInDays = tokenExpirationInDays;
        Jwt = jwt;
    }
    
    public string TokenSecret { get; init; }
    public int TokenExpirationInDays { get; init; }
    public JwtSetting Jwt { get; init; }
    
    public static AuthenticationSetting FromConfiguration(IConfiguration configuration)
    {
        var tokenSecret = configuration[$"{nameof(AuthenticationSetting)}:{nameof(TokenSecret)}"]!;
        var tokenExpirationInDays = int.Parse(configuration[$"{nameof(AuthenticationSetting)}:{nameof(TokenExpirationInDays)}"]!);
        var jwt = JwtSetting.FromConfiguration(configuration.GetSection($"{nameof(AuthenticationSetting)}"));

        return new AuthenticationSetting(tokenSecret, tokenExpirationInDays, jwt);
    }
}