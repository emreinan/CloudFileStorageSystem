using AuthenticationAPI.Application.Features.Auth.Rules;
using AuthenticationAPI.Application.Services.Auth;
using AuthenticationAPI.Persistence.Context;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AuthenticationAPI.Application.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationAPI;

public static class AuthenticationAPIRegistrationServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("AuthenticationDb")));

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthBusinessRules>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        AddJwtAuthentication(services, configuration);
        AddSwaggerGen(services);

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load(nameof(AuthenticationAPI)));

        return services;
    }

    private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
                    ?? throw new InvalidOperationException("TokenOptions cant found in configuration");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidIssuer = tokenOptions.Issuer,
                   ValidAudience = tokenOptions.Audience,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
               };
           });
    }

    private static void AddSwaggerGen(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(
                name: "Bearer",
                securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. " +
                        "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                        "\r\n\r\nExample: \"Bearer 12345.54321\""
                }
            );
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] { }
            }
                }
            );
        });
    }
}
