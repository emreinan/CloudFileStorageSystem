using AuthenticationAPI.Application.Security;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAPI.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);

        builder.HasIndex(u => u.Email).IsUnique();

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash("1234", out passwordHash, out passwordSalt);
        builder.HasData(
            new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@admin.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Role.Admin
            });
    }
}
