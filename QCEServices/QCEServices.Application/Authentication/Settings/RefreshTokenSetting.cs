namespace QCEServices.Application.Authentication.Settings;

public sealed class RefreshTokenSetting
{
    public const string ConfigurationSectionName = "Authentication:RefreshToken";
    
    public string Secret { get; set; } = string.Empty;
    public int ExpirationDays { get; set; }
}