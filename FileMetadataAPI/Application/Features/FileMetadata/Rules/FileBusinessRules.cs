using FileMetadataAPI.Application.Exceptions.Types;
using FileMetadataAPI.Application.Features.FileMetadata.Constans;
using FileMetadataAPI.Infrastructure.Context;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.FileMetadata.Rules;

public class FileBusinessRules
{
    public void FileIsExists(File? file)
    {
        if (file is null)
        {
            throw new NotFoundException(FileMetadataErrorMessage.FileNotFound);
        }
    }
}
