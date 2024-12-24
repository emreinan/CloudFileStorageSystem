using CloudFileStorageMVC.Services.Auth;
using CloudFileStorageMVC.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace CloudFileStorageMVC
{
    public static class ClientMvcServicesRegistration
    {
        public static IServiceCollection AddClientMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddJwtAuthentication(configuration);
            AddServices(services);
            GetApiUrl(services, configuration);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, CookieTokenService>();
            services.AddScoped<IAuthService, HttpAuthService>();
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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

                   options.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context => // her requestte token kontrolü yapar.
                       {
                           var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                           context.Token = tokenService.GetAccessToken();

                           return Task.CompletedTask;
                       },

                       OnChallenge = async context => // token süresi dolduğunda refresh token ile yeni token almak için kullanılır.
                       {
                           var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                           var refreshToken = tokenService.GetRefreshToken();

                           if (string.IsNullOrEmpty(refreshToken)) // Refresh token yoksa login sayfasına yönlendir.
                           {
                               context.HandleResponse();
                               context.Response.Redirect("/Login");
                           }

                           try
                           {
                               var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                               var tokenResponse = await authService.RefreshTokenAsync(refreshToken);
                               tokenService.SetAccessToken(tokenResponse.AccessToken);
                               tokenService.SetRefreshToken(tokenResponse.RefreshToken);
                               context.HandleResponse();
                               context.Response.Redirect("/Index"); // main sayfa yani ındex'e gider.
                           }
                           catch
                           {
                               context.HandleResponse();
                               context.Response.Redirect("/Login");
                           }
                       }
                   };
               });
            return services;
        }

        private static void GetApiUrl(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("GetewayApiClient", client =>
            {
                string apiUrl = configuration["GatewayApiUrl"] ?? throw new InvalidOperationException("GatewayApi URL is missing");
                client.BaseAddress = new Uri(apiUrl);
            });

            services.AddHttpClient("AuthApiClient", client =>
            {
                string apiUrl = configuration["AuthApiUrl"] ?? throw new InvalidOperationException("AuthApi URL is missing");
                client.BaseAddress = new Uri(apiUrl);
            });
        }
    }
}
