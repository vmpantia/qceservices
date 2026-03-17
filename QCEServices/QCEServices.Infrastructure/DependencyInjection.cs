using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QCEServices.Infrastructure.DataAccess.Contexts;
using QCEServices.Infrastructure.DataAccess.Interceptors;

namespace QCEServices.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(IConfiguration configuration)
        {
            services.AddInterceptors();
            services.AddDbContexts(configuration);
        }

        private void AddInterceptors()
        {
            services.AddSingleton<AuditEntitiesInterceptor>();
        }

        private void AddDbContexts(IConfiguration configuration)
        {
            services.AddDbContext<QCEServicesDbContext>((sp, opt) =>
            {
                var interceptor = sp.GetRequiredService<AuditEntitiesInterceptor>();

                opt.UseSqlServer(configuration.GetConnectionString("MigrationDb"))
                    .AddInterceptors(interceptor);
            });
        }
    }
}