using FileStorageAPI.Application.Features.Rules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace FileStorageAPI.Application
{
    public static class FileStorageApiApplicationServices
    {
        public static IServiceCollection AddFileStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.Load(nameof(FileStorageAPI)));

            services.AddHttpContextAccessor();
            services.AddScoped<FileStorageBusinessRules>();

            return services;
        }
    }
}
