using FileMetadataAPI.Infrastructure.Context;
using File= FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.Rules;

public class FileBusinessRules(FileMetaDataDbContext fileMetaDataDbContext)
{
    public async Task FileIsExists(File? file)
    {
        if (file is null)
        {
            throw new Exception("File not found");
        }
    }
}
