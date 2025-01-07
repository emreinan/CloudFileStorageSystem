using CloudFileStorageMVC.Dtos.Auth;
using CloudFileStorageMVC.Util.ExceptionHandling;
using System.Net;
using System.Net.Http.Json;

namespace CloudFileStorageMVC.Services.Auth;

public class HttpAuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GetewayApiClient");

    public async Task<TokenResponse> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginDto);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var apiError = await response.Content.ReadFromJsonAsync<ApiError>();
            throw new ApiException(apiError);
        }
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var refreshTokenRequest = new RefreshTokenRequest { Token = refreshToken };
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/refresh", new { refreshTokenRequest });
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }

    public async Task<TokenResponse> RegisterAsync(RegisterDto registerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/register", registerDto);
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }
}
