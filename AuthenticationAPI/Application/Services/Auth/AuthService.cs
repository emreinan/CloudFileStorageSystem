using AuthenticationAPI.Application.Jwt;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Application.Services.Auth;

public class AuthService(IConfiguration configuration, AuthDbContext authDb) : IAuthService
{
    public string CreateAccessToken(User user)
    {

        TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
            ?? throw new InvalidOperationException("TokenOptions cant found in configuration");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(tokenOptions.AccessTokenExpiration);

        var jwt = new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: expires,
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.WriteToken(jwt);
        return accessToken;
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(User user)
    {
        string refreshToken = Guid.NewGuid().ToString();
        var newRefreshToken = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["TokenOptions:RefreshTokenExpiration"])),
        };

        await authDb.RefreshTokens.AddAsync(newRefreshToken);
        await authDb.SaveChangesAsync();
        return newRefreshToken;
    }

    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken)
    {
        refreshToken.RevokedDate = DateTime.UtcNow;
        var newRefreshToken = await CreateRefreshTokenAsync(user);
        authDb.RefreshTokens.Update(refreshToken);
        await authDb.SaveChangesAsync();
        return newRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(int userId)
    {
        var oldRefreshTokens = await authDb.RefreshTokens.Where(rt => rt.UserId == userId && rt.ExpiryDate <= DateTime.UtcNow).ToListAsync();
        authDb.RefreshTokens.RemoveRange(oldRefreshTokens);
        await authDb.SaveChangesAsync();
    }
}
