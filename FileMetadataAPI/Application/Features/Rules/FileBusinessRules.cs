using FileMetadataAPI.Infrastructure.Context;
using File= FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.Rules;

public class FileBusinessRules
{
    public void FileIsExists(File? file)
    {
        if (file is null)
        {
            throw new Exception("File not found");
        }
    }
}
