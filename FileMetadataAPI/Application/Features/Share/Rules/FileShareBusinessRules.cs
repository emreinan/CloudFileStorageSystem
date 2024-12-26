using FileShare = FileMetadataAPI.Domain.Entities.FileShare;
using File = FileMetadataAPI.Domain.Entities.File;
using System.Security.Claims;
using FileMetadataAPI.Application.Exceptions.Types;
using FileMetadataAPI.Application.Features.Share.Constans;

namespace FileMetadataAPI.Application.Features.Share.Rules;

public class FileShareBusinessRules
{
    public void FileShareIsNull(FileShare? fileShare)
    {
        if (fileShare is null)
        {
            throw new NotFoundException(FileShareErrorMessage.FileShareNotFound);
        }
    }
    public void FileIsExist(File? file)
    {
        if (file is null)
        {
            throw new NotFoundException(FileShareErrorMessage.FileNotFound);
        }
    }
    public int ClaimIsNull(Claim? claim)
    {
        if (claim is null)
        {
            throw new AuthorizationException(FileShareErrorMessage.ClaimNotFound);
        }
        return int.Parse(claim.Value);
    }
    public void IsMatchedUserId(int userId, int requestUserId)
    {
        if (userId != requestUserId)
        {
            throw new AuthorizationException(FileShareErrorMessage.NotAllowedToShare);
        }
    }
}
