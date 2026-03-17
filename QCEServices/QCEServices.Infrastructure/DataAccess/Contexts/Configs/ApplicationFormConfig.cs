using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCEServices.Domain.Entities;

namespace QCEServices.Infrastructure.DataAccess.Contexts.Configs;

public class ApplicationFormConfig : IEntityTypeConfiguration<ApplicationForm>
{
    public void Configure(EntityTypeBuilder<ApplicationForm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Type, x.Status }).IsUnique();
    }
}