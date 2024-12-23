using AuthenticationAPI.Application.Features.Auth.Constants;
using AuthenticationAPI.Application.Security;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AuthenticationAPI.Application.Features.Auth.Rules;

public class AuthBusinessRules(AuthDbContext authDb)
{
    public void UserShouldExist(User? user)
    {
        if (user is null)
            throw new Exception(AuthErrorMessages.UserNotFound);
    }

    public void PasswordShouldMatch(string password, User user)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new Exception(AuthErrorMessages.InvalidPassword);
    }

    public async Task EmailShouldBeUnique(string email)
    {
        if (await authDb.Users.AnyAsync(u => u.Email == email))
            throw new Exception(AuthErrorMessages.EmailInUse);
    }

    public void RefreshTokenShouldExist(RefreshToken? refreshToken)
    {
        if (refreshToken is null)
            throw new Exception(AuthErrorMessages.RefreshTokenNotFound);
    }

    public void RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate is not null || refreshToken.ExpiryDate <= DateTime.UtcNow)
            throw new Exception(AuthErrorMessages.InvalidRefreshToken);
    }
}
