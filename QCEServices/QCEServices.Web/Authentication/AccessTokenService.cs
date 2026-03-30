namespace QCEServices.Web.Authentication;

public sealed class AccessTokenService : IAccessTokenService
{
    private string? _value;

    public string? Token => _value;
    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_value);
    
    public void SetToken(string token) => _value = token;

    public void ClearToken() => _value = null;
}