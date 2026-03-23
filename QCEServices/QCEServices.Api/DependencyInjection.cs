using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

namespace QCEServices.Api;

internal static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        internal void AddApi(IConfiguration configuration)
        {
            services.AddHttpContextAccessor();  
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenWithAuth(configuration);
            services.AddAuthorization();
            services.AddJwtAuthentication(configuration);
        }
        
        private void AddSwaggerGenWithAuth(IConfiguration configuration)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                            TokenUrl = new Uri(configuration["Keycloak:TokenUrl"]!),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "openid" },
                                { "profile", "profile" }
                            }
                        }
                    }
                });
                
                opt.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Keycloak", document), 
                        []
                    }
                });
            });
        }

        private void AddJwtAuthentication(IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Authority = "http://localhost:18080/realms/qrservices-auth";
                    opt.Audience  = "account";
                    opt.RequireHttpsMetadata = false;
                });
        }
    }
}