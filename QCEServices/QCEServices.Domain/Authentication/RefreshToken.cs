using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Domain.Authentication;

public sealed class RefreshToken : IToken
{
    public const string CookieName = "refresh_token";
    public string Value { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}