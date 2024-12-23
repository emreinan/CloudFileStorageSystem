using FileMetadataAPI.Application.Features.Rules;
using FileMetadataAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<FileBusinessRules>();

        return services;
    }
}
