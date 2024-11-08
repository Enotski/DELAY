using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Infrastructure.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure.Auth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services)
        {
            services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
            services.AddSingleton<IVkAuthService, VkAuthService>();
            services.AddSingleton<ITokensService, JwtTokensService>();

            return services;
        }
    }
}
