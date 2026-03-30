using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace QCEServices.Web.Authentication;

public sealed class CustomAuthenticationStateProvider(IAccessTokenService accessTokenService) : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = accessTokenService.IsAuthenticated 
            ? new ClaimsIdentity() 
            : GetClaimsIdentity(accessTokenService.Token!);
        
        var user = new ClaimsPrincipal(identity);   
        return Task.FromResult(new AuthenticationState(user));
    }

    public void MarkUserAsAuthenticated(string token)
    {
        accessTokenService.SetToken(token);
        var identity = GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);
        var authUser = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authUser);
    }

    public void MarkUserAsLoggedOut()
    {
        accessTokenService.ClearToken();
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        var authUser = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authUser);
    }

    private ClaimsIdentity GetClaimsIdentity(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return new ClaimsIdentity(jwtToken.Claims, "jwt");
    }
}