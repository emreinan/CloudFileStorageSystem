namespace CloudFileStorageMVC.Services.Token;

public interface ITokenService
{
    public string? GetAccessToken();
    public string? GetRefreshToken();

    public void SetAccessToken(string accessToken);
    public void SetRefreshToken(string refreshToken);

    public void RemoveAccessToken();
    public void RemoveRefreshToken();
}
