using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCEServices.Domain.Entities;

namespace QCEServices.Infrastructure.DataAccess.Contexts.Configs;

public class TokenConfig : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.HasIndex(t => new { t.Value, t.IsRevoked, t.ExpiresAt }).IsUnique();

        builder.HasOne(t => t.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(t => t.UserId)
            .IsRequired();
    }
}