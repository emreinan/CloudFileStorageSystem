using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Security;
using AuthenticationAPI.Application.Services.Auth;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.Enums;
using AuthenticationAPI.Persistence.Context;
using MediatR;

namespace AuthenticationAPI.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    
    internal class RegisterCommandHandler(
        AuthDbContext authDb,
        AuthBusinessRules authBusinessRules,
        IAuthService authService
        ) : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await authBusinessRules.EmailShouldBeUnique(request.Email);

            HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
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
