using Microsoft.Extensions.Configuration;

namespace QCEServices.Application.Authentication;

public sealed class JwtSetting
{
    public JwtSetting() { }

    private JwtSetting(string secret, string issuer, string audience, int expirationInMinutes)
    {
        Secret = secret;
        Issuer = issuer;
        Audience = audience;
        ExpirationInMinutes = expirationInMinutes;
    }
    
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationInMinutes { get; set; }

    public static JwtSetting FromConfiguration(IConfiguration section)
    {
        var secret = section[$"{nameof(JwtSetting)}:{nameof(Secret)}"]!;
        var issuer = section[$"{nameof(JwtSetting)}:{nameof(Issuer)}"]!;
        var audience = section[$"{nameof(JwtSetting)}:{nameof(Audience)}"]!;
        var expirationInMinutes = int.Parse(section[$"{nameof(JwtSetting)}:{nameof(ExpirationInMinutes)}"]!);

        return new JwtSetting(secret, issuer, audience, expirationInMinutes);
    }
}