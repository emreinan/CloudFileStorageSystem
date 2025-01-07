using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;

namespace FileMetadataAPI.Persistence.Configurations;

public class FileShareConfiguration : IEntityTypeConfiguration<FileShare>
{
    public void Configure(EntityTypeBuilder<FileShare> builder)
    {
        builder.ToTable("FileShares");
        builder.HasKey(fs => fs.Id);
        builder.Property(fs => fs.FileId).IsRequired();
        builder.Property(fs => fs.UserId).IsRequired();
        builder.Property(fs => fs.PermissionLevel).IsRequired().HasConversion<string>().HasMaxLength(50);

        builder.HasOne(fs => fs.File)
               .WithMany(f => f.FileShares)
               .HasForeignKey(fs => fs.FileId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

