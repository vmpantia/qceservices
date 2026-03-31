namespace QCEServices.Application.Authentication.Settings;

public sealed class AccessTokenSetting
{
    public const string ConfigurationSectionName = "Authentication:AccessToken";
    
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; }
}