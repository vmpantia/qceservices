using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCEServices.Domain.Entities;

namespace QCEServices.Infrastructure.DataAccess.Contexts.Configs;

public class MarriageLicenseConfig : IEntityTypeConfiguration<MarriageLicense>
{
    public void Configure(EntityTypeBuilder<MarriageLicense> builder)
    {
        builder.HasKey(ml => ml.Id);

        builder.OwnsOne(ml => ml.Groom, groom =>
        {
            groom.OwnsOne(p => p.Name);
            groom.OwnsOne(p => p.BirthPlace);
            groom.OwnsOne(p => p.Residence);

            groom.OwnsOne(p => p.Parents, parents =>
            {
                parents.OwnsOne(p => p.Father, father =>
                {
                    father.OwnsOne(p => p.Name);
                    father.OwnsOne(p => p.Residence);
                });

                parents.OwnsOne(p => p.Mother, mother =>
                {
                    mother.OwnsOne(p => p.Name);
                    mother.OwnsOne(p => p.Residence);
                });
            });
        });

        builder.OwnsOne(ml => ml.Bride, bride =>
        {
            bride.OwnsOne(p => p.Name);
            bride.OwnsOne(p => p.BirthPlace);
            bride.OwnsOne(p => p.Residence);

            bride.OwnsOne(p => p.Parents, parents =>
            {
                parents.OwnsOne(p => p.Father, father =>
                {
                    father.OwnsOne(p => p.Name);
                    father.OwnsOne(p => p.Residence);
                });

                parents.OwnsOne(p => p.Mother, mother =>
                {
                    mother.OwnsOne(p => p.Name);
                    mother.OwnsOne(p => p.Residence);
                });
            });
        });
        
        builder.HasOne(ml => ml.ApplicationForm)
            .WithOne(af => af.MarriageLicense)
            .HasForeignKey<MarriageLicense>(ml => ml.ApplicationFormId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}