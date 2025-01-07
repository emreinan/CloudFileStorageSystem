using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Services.Auth;
using AuthenticationAPI.Core.Security;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.Enums;
using AuthenticationAPI.Persistence.Context;
using MediatR;

namespace AuthenticationAPI.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserRegisterDto Register { get; set; }

    internal class RegisterCommandHandler(
        AuthDbContext authDb,
        AuthBusinessRules authBusinessRules,
        IAuthService authService
        ) : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await authBusinessRules.EmailShouldBeUnique(request.Register.Email);

            HashingHelper.CreatePasswordHash(request.Register.Password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                Name = request.Register.Name,
                Email = request.Register.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Role.User
            };

            await authDb.Users.AddAsync(user);
            await authDb.SaveChangesAsync();

            var accessToken = authService.CreateAccessToken(user);
            var refreshToken = await authService.CreateRefreshTokenAsync(user);

            return new RegisteredResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
