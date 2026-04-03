using System.Reflection;
using FluentValidation;
using QCEServices.Web.Contracts.Services.Apis;
using QCEServices.Web.Services.Apis;

namespace QCEServices.Web;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddApiServices()
        {
            services.AddHttpClient<IAuthenticationApiService, AuthenticationApiService>(client =>
            {
                client.BaseAddress = new Uri("http://qceservices.api:8080");
            });
            services.AddHttpClient<IMarriageLicenseApiService, MarriageLicenseApiService>(client =>
            {
                client.BaseAddress = new Uri("http://qceservices.api:8080");
            });
        }
    }
}