using FileMetadataAPI.Application.Features.Rules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace FileMetadataAPI.Application
{
    public static class FileMetadataApiApplicationServices
    {
        public static IServiceCollection AddFileMetadataAppRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<FileMetadataBusinessRules>();
            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.Load(nameof(FileMetadataAPI)));

            return services;
        }

    }
}
