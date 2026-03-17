using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCEServices.Domain.Entities;

namespace QCEServices.Infrastructure.DataAccess.Contexts.Configs;

public class MarriageLicenseConfig : IEntityTypeConfiguration<MarriageLicense>
{
    public void Configure(EntityTypeBuilder<MarriageLicense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.ApplicationFormId).IsUnique();

        builder.OwnsOne(x => x.Groom, groom =>
        {
            groom.OwnsOne(x => x.Name);
            groom.OwnsOne(x => x.BirthPlace);
            groom.OwnsOne(x => x.Residence);

            groom.OwnsOne(x => x.Parents, parents =>
            {
                parents.OwnsOne(x => x.Father, father =>
                {
                    father.OwnsOne(x => x.Name);
                    father.OwnsOne(x => x.Residence);
                });

                parents.OwnsOne(x => x.Mother, mother =>
                {
                    mother.OwnsOne(x => x.Name);
                    mother.OwnsOne(x => x.Residence);
                });
            });
        });

        builder.OwnsOne(x => x.Bride, bride =>
        {
            bride.OwnsOne(x => x.Name);
            bride.OwnsOne(x => x.BirthPlace);
            bride.OwnsOne(x => x.Residence);

            bride.OwnsOne(x => x.Parents, parents =>
            {
                parents.OwnsOne(x => x.Father, father =>
                {
                    father.OwnsOne(x => x.Name);
                    father.OwnsOne(x => x.Residence);
                });

                parents.OwnsOne(x => x.Mother, mother =>
                {
                    mother.OwnsOne(x => x.Name);
                    mother.OwnsOne(x => x.Residence);
                });
            });
        });
    }
}