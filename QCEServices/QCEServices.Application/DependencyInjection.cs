using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QCEServices.Application.Behaviors;

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