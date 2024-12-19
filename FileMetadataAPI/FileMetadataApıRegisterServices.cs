using FileMetadataAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI;

public static class FileMetadataApıRegisterServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileMetaDataDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FileMetadataDb")));
        return services;
    }
}
