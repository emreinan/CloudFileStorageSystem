using AuthenticationAPI.Domain.Entities;

namespace AuthenticationAPI.Application.Services.Auth;

public interface IAuthService
{
    string CreateAccessToken(User user);
    Task<RefreshToken> CreateRefreshTokenAsync(User user);
    Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken);
    Task DeleteOldRefreshTokens(int userId);
}
