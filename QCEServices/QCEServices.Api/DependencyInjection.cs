using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QCEServices.Application.Authentication.Settings;
using QCEServices.Domain.Authentication;

namespace QCEServices.Api;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddApi(IConfiguration configuration)
        {
            services.AddSettings(configuration);
            services.AddJwtAuthentication(configuration);
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private void AddSettings(IConfiguration configuration)
        {
            services.Configure<AccessTokenSetting>(opt => configuration.GetSection(AccessTokenSetting.ConfigurationSectionName).Bind(opt));
            services.Configure<RefreshTokenSetting>(opt => configuration.GetSection(RefreshTokenSetting.ConfigurationSectionName).Bind(opt));
        }

        private void AddJwtAuthentication(IConfiguration configuration)
        {
            var accessTokenSetting = configuration
                .GetSection(AccessTokenSetting.ConfigurationSectionName)
                .Get<AccessTokenSetting>()!;
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = accessTokenSetting.Issuer,
                        ValidAudience = accessTokenSetting.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSetting.Secret))
                    };
                    opt.Events.OnMessageReceived = context =>
                    {
                        context.Token = context.HttpContext.Request.Cookies[AccessToken.CookieName];
                        return Task.CompletedTask;
                    };
                });
        }
    }
}