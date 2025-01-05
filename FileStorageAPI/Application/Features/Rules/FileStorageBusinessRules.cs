using FileStorageAPI.Application.Exceptions.Types;
using FileStorageAPI.Application.Features.Constants;
using System.Security.Claims;

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
}
