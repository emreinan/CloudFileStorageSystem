using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Services.Auth;
using AuthenticationAPI.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthenticationAPI;

public static class AuthenticationAPIRegistrationServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("AuthenticationDb")));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthBusinessRules>();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
