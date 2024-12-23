using AuthenticationAPI.Application.Features.Auth.Constants;
using System.Security.Claims;

namespace FileStorageAPI.Application.Features.Rules;

public class FileStorageBusinessRules(IHttpContextAccessor httpContextAccessor)
{
    public int GetUserIdClaim()
    {
        var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            throw new Exception(FileStorageErrorMessages.UserNotAuthorized);
        }
        return int.Parse(userIdClaim.Value);
    }
}
