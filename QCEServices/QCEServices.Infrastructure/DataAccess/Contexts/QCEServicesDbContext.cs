using Microsoft.EntityFrameworkCore;
using QCEServices.Domain.Entities;

namespace QCEServices.Infrastructure.DataAccess.Contexts;

public sealed class QCEServicesDbContext(DbContextOptions<QCEServicesDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationForm> ApplicationForms => Set<ApplicationForm>();
    public DbSet<MarriageLicense> MarriageLicenses => Set<MarriageLicense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QCEServicesDbContext).Assembly);
    }
}