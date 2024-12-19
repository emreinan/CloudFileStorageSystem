using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;
using File= FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Infrastructure.Configurations;

public class FileShareConfiguration : IEntityTypeConfiguration<FileShare>
{
    public void Configure(EntityTypeBuilder<FileShare> builder)
    {
        builder.ToTable("FileShares");
        builder.HasKey(fs => new { fs.FileId, fs.UserId });
        builder.Property(fs => fs.Permission).IsRequired().HasMaxLength(50);

        builder.HasOne<File>()
            .WithMany()
            .HasForeignKey(fs => fs.FileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

