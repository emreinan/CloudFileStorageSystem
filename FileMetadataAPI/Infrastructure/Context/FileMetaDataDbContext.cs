using Microsoft.EntityFrameworkCore;
using FileShare = FileMetadataAPI.Domain.Entities.FileShare;
using File = FileMetadataAPI.Domain.Entities.File;
using System.Reflection;


namespace FileMetadataAPI.Infrastructure.Context;

public class FileMetaDataDbContext(DbContextOptions<FileMetaDataDbContext> options) : DbContext(options)
{
    public DbSet<File> Files { get; set; }
    public DbSet<FileShare> FileShares { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
