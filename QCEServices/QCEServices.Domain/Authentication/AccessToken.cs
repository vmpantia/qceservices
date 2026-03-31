using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Domain.Authentication;

public sealed class AccessToken : IToken
{
    public const string CookieName = "access_token";
    public string Value { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}