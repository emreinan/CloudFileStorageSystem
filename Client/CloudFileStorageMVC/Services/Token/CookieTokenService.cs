using Microsoft.AspNetCore.Http;

namespace CloudFileStorageMVC.Services.Token;

public class CookieTokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
{
    public string? GetAccessToken()
    {
        return httpContextAccessor.HttpContext.Request.Cookies["access_token"];
    }

    public string? GetRefreshToken()
    {
        return httpContextAccessor.HttpContext.Request.Cookies["refresh_token"];
    }

    public void SetAccessToken(string accessToken)
    {
        httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", accessToken, new CookieOptions
        {
            Secure = true, // cookie sadece https üzerinden çalışır
            Expires = DateTimeOffset.UtcNow.AddMinutes(10), // 10 dakika sonra cookie silinir
            SameSite = SameSiteMode.None, // cookie sadece kendi domain'ine gönderilir
            HttpOnly = true, // cookie sadece http üzerinden erişilebilir
        });
    }

    public void SetRefreshToken(string refreshToken)
    {
        httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
        {
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(10),
            SameSite = SameSiteMode.None,
            HttpOnly = true,
        });
    }

    public void RemoveAccessToken()
    {
        //httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");

        httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(-1),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
        });
    }

    public void RemoveRefreshToken()
    {
        //httpContextAccessor.HttpContext.Response.Cookies.Delete("refresh_token");

        httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", "", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(-1),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
        });
    }
}
