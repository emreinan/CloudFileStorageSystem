using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using AuthenticationAPI.Application.Features.Users.Rules;


namespace AuthenticationAPI.Application;

public static class AuthAPIApplicationServices
{
    public static IServiceCollection AddAuthApiAppRegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthBusinessRules>();
        services.AddScoped<UserBusinessRules>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load(nameof(AuthenticationAPI)));
        return services;
    }
   
}
