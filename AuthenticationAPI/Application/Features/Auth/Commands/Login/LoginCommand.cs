using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Services.Auth;
using AuthenticationAPI.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }

    // Nested yapısı
    internal class LoginCommandHandler(
        AuthDbContext authDb,
        AuthBusinessRules authBusinessRules,
        IAuthService authService
        ) : IRequestHandler<LoginCommand, LoggedResponse>
    {
        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await authDb.Users.Include(u=>u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == request.Email);

            authBusinessRules.UserShouldExist(user);

            authBusinessRules.PasswordShouldMatch(request.Password, user!);

            await authService.DeleteOldRefreshTokens(user!.Id);

            var accessToken = authService.CreateAccessToken(user!);
            var refreshToken = await authService.CreateRefreshTokenAsync(user!);

            return new LoggedResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
