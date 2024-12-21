using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Services.Auth;
using AuthenticationAPI.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static AuthenticationAPI.Application.Features.Auth.Commands.RefreshToken.RefreshTokenCommand;

namespace AuthenticationAPI.Application.Features.Auth.Commands.RefreshToken;

public partial class RefreshTokenCommand : IRequest<RefreshedTokenResponse>
{
    public string Token { get; set; }

    internal class RefreshTokenCommandHandler(
        IAuthService authService,
        AuthDbContext dbContext,
        AuthBusinessRules authBusinessRules
        ) : IRequestHandler<RefreshTokenCommand, RefreshedTokenResponse>
    {
        public async Task<RefreshedTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == request.Token);

            authBusinessRules.RefreshTokenShouldExist(refreshToken);
            
            authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == refreshToken!.UserId);

            authBusinessRules.UserShouldExist(user);

            await authService.RotateRefreshToken(user!, refreshToken!);

            var accessToken = authService.CreateAccessToken(user!);

            return new RefreshedTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken!.Token
            };
        }
    }
}

