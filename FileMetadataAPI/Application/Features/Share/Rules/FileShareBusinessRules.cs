using FileShare = FileMetadataAPI.Domain.Entities.FileShare;
using File = FileMetadataAPI.Domain.Entities.File;
using System.Security.Claims;

namespace FileMetadataAPI.Application.Features.Share.Rules;

public class FileShareBusinessRules
{
    public void FileShareIsNull(FileShare fileShare)
    {
        if (fileShare is null)
        {
            throw new KeyNotFoundException("FileShare not found.");
        }
    }
    public void FileIsExist(File file)
    {
        if (file is null)
        {
            throw new KeyNotFoundException("File not found.");
        }
    }
    public int ClaimIsNull(Claim claim)
    {
        if (claim is null)
        {
            throw new UnauthorizedAccessException("Claim not found.");
        }
        return int.Parse(claim.Value);
    }
    public void IsMatchedUserId(int userId, int requestUserId)
    {
        if (userId != requestUserId)
        {
            throw new UnauthorizedAccessException("You are not allowed to share this file.");
        }
    }
}
