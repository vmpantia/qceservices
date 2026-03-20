using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QCEServices.Application.Common.Authentication;
using QCEServices.Application.Common.Behaviors;
using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddApplication()
        {
            services.AddMediatR();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
        }

        private void AddMediatR()
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(DbTransactionPipelineBehavior<,>));
            });
        }
    }
}