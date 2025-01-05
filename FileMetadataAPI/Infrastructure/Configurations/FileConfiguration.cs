using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Infrastructure.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("Files");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.OwnerId).IsRequired(); 
        builder.Property(f => f.Name).IsRequired().HasMaxLength(200);
        builder.Property(f => f.Description).HasMaxLength(500);
        builder.Property(f => f.UploadDate).IsRequired();
        builder.Property(f => f.SharingType).IsRequired().HasConversion<string>().HasMaxLength(50);

    }
}


