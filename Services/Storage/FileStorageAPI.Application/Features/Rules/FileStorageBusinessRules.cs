using FileStorageAPI.Application.Features.Constants;
using FileStorageAPI.Core.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace FileStorageAPI.Application.Features.Rules;

public class FileStorageBusinessRules
{
    public void UploadPathIsExists(string uploadPath)
    {
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
    }
    public void FileIsExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            throw new BusinessException(FileStorageErrorMessages.FileAlreadyExists);
        }
    }
    public void FileSizeIsValid(IFormFile file)
    {
        const long maxSizeInBytes = 5 * 1024 * 1024;
        if (file.Length > maxSizeInBytes)
        {
            throw new BusinessException(FileStorageErrorMessages.FileSizeExceeded);
        }
    }

}
