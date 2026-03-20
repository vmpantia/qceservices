using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCEServices.Domain.Entities;

namespace QCEServices.Infrastructure.DataAccess.Contexts.Configs;

public class ApplicationFormConfig : IEntityTypeConfiguration<ApplicationForm>
{
    public void Configure(EntityTypeBuilder<ApplicationForm> builder)
    {
        builder.HasKey(af => af.Id);
        
        builder.HasIndex(af => new { af.Type, af.Status }).IsUnique();

        builder.HasOne(af => af.Applicant)
            .WithMany(u => u.ApplicationForms)
            .HasForeignKey(af => af.ApplicantId);
    }
}