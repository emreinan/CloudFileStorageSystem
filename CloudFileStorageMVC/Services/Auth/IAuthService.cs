using CloudFileStorageMVC.Dtos.Auth;

namespace CloudFileStorageMVC.Services.Auth;

public interface IAuthService
{
    public Task<TokenResponse> LoginAsync(LoginDto loginDto);
    public Task<TokenResponse> RegisterAsync(RegisterDto registerDto);
    public Task<TokenResponse> RefreshTokenAsync(string refreshToken);
}
