using AuthenticationAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI;

public static class AuthenticationAPIRegistrationServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthenticationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("AuthenticationDb")));

        return services;
    }
}
