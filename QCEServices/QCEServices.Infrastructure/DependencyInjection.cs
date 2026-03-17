using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QCEServices.Infrastructure.DataAccess.Contexts;

namespace QCEServices.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(IConfiguration configuration)
        {
            services.AddDbContexts(configuration);
        }

        private void AddDbContexts(IConfiguration configuration)
        {
            services.AddDbContext<QCEServicesDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("MigrationDb"));
            });
        }
    }
}