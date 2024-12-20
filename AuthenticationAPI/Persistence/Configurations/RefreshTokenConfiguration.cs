using AuthenticationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAPI.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens").HasKey(rt => rt.Id);
        builder.Property(rt => rt.Id).ValueGeneratedOnAdd();
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.ExpiryDate).IsRequired();
        builder.Property(rt => rt.CreatedaAt).IsRequired();
        builder.Property(rt => rt.RevokedDate).IsRequired(false);

        builder.HasIndex(rt => rt.Token).IsUnique();

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}