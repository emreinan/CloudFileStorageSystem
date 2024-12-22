using System.Reflection;

namespace FileStorageAPI;

public static class FileStorageAPIRegistationServices
{
    public static IServiceCollection AddFileStorageServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });



        return services;
    }
}
