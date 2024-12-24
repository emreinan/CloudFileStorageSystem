using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Infrastructure.Context;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FileMetadataAPI;

public static class FileMetadataApıRegisterServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileMetaDataDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FileMetadataDb")));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<FileBusinessRules>();
        services.AddHttpContextAccessor();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load(nameof(FileMetadataAPI)));

        services.AddHttpClient("FileStorageApiClient", client =>
        {
            string apiUrl = configuration["FileStorageApiUrl"] ?? throw new InvalidOperationException();
            client.BaseAddress = new Uri(apiUrl);
        });

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        

        return services;
    }
}
