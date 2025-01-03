using FileMetadataAPI.Application.Exceptions.Types;
using FileMetadataAPI.Application.Features.FileMetadata.Constans;
using FileMetadataAPI.Domain.Enums;
using MediatR;
using System.Security.Claims;
using File = FileMetadataAPI.Domain.Entities.File;

namespace FileMetadataAPI.Application.Features.FileMetadata.Rules;

public class FileMetadataBusinessRules(IHttpContextAccessor httpContextAccessor)
{
    public void FileIsExists(File? file)
    {
        if (file is null)
        {
            throw new NotFoundException(FileMetadataErrorMessage.FileNotFound);
        }
    }
    public int GetUserIdClaim()
    {
        var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            throw new AuthorizationException(FileMetadataErrorMessage.UserNotAuthorized);
        }
        return int.Parse(userIdClaim.Value);
    }
    public bool IsUserAdmin()
    {
        var roleClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
        return roleClaim?.Value == "Admin";
    }
    public static T ConvertToEnum<T>(string value) where T : struct, Enum
    {
        if (Enum.TryParse(value, true, out T result))
        {
            return result;
        }
        throw new BusinessException($"Invalid value: {value} for enum type {typeof(T).Name}");
    }
}
