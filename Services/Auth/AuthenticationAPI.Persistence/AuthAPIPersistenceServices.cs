using AuthenticationAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAPI.Persistence
{
    public static class AuthAPIPersistenceServices
    {
        public static IServiceCollection AddAuthApiPresentationRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("AuthenticationDb")));
            return services;
        }
    }
}
