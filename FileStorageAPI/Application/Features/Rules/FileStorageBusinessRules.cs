using AuthenticationAPI.Application.Features.Auth.Constants;
using FileStorageAPI.Application.Exceptions.Types;
using System.Security.Claims;

namespace FileStorageAPI.Application.Features.Rules;

public class FileStorageBusinessRules(IHttpContextAccessor httpContextAccessor)
{
    public int GetUserIdClaim()
    {
        var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            throw new AuthorizationException(FileStorageErrorMessages.UserNotAuthorized);
        }
        return int.Parse(userIdClaim.Value);
    }
}
