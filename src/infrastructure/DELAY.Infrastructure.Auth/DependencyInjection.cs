using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Infrastructure.Auth.Services;
using DELAY.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure.Auth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
            services.AddSingleton<IVkAuthService, VkAuthService>();
            services.AddSingleton<ITokensService, JwtTokensService>();

            services.Configure<SecurityConfig>(options => configuration.GetSection(SecurityConfig.SectionName).Bind(options));
            services.Configure<TokensSettings>(options => configuration.GetSection(TokensSettings.SectionName).Bind(options));

            services.Configure<GoogleApiSecrets>(options => configuration.GetSection(GoogleApiSecrets.SectionName).Bind(options));
            services.Configure<VkApiSecrets>(options => configuration.GetSection(VkApiSecrets.SectionName).Bind(options));

            return services;
        }
    }
}
