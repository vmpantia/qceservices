namespace QCEServices.Web.Authentication;

public interface IAccessTokenService
{
    string? Token { get; }
    bool IsAuthenticated { get; }
    void SetToken(string token);
    void ClearToken();
}