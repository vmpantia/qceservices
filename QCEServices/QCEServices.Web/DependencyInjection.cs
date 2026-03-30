using Microsoft.AspNetCore.Components.Authorization;
using QCEServices.Web.Authentication;

namespace QCEServices.Web;

public static class DependencyInjection
{
    public static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccessTokenService, AccessTokenService>();
        services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5001/");
        });
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    }
}