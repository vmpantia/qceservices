using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using QCEServices.Application.Authentication;

namespace QCEServices.Api;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddApi(IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration);
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenWithAuth();
        }
        
        private void AddSwaggerGenWithAuth()
        {
            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                opt.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document)] = []
                });
            });
        }

        private void AddJwtAuthentication(IConfiguration configuration)
        {
            var authSetting = AuthenticationSetting.FromConfiguration(configuration);
            services.AddSingleton(authSetting);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = authSetting.Jwt.Issuer,
                        ValidAudience = authSetting.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.Jwt.Secret))
                    };
                });
        }
    }
}