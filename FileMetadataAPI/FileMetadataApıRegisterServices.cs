using FileMetadataAPI.Infrastructure.Context;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FileMetadataAPI.Application.Features.FileMetadata.Rules;
using FileMetadataAPI.Application.Features.Share.Rules;
using FileMetadataAPI.Application.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FileMetadataAPI;

public static class FileMetadataApıRegisterServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileMetaDataDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FileMetadataDb")));

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        AddJwtAuthentication(services,configuration);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<FileBusinessRules>();
        services.AddScoped<FileShareBusinessRules>();
        services.AddHttpContextAccessor();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load(nameof(FileMetadataAPI)));

        services.AddHttpClient("FileStorageApiClient", client =>
        {
            string apiUrl = configuration["FileStorageApiUrl"] ?? throw new InvalidOperationException();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Add("x-source", "FileMetadataAPI");
        });

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

        services.AddAuthorization();
    }
}
