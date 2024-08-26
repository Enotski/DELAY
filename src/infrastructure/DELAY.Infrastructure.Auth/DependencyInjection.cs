using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Infrastructure.Auth.Services;
using DELAY.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.Configure<TokensSettings>(options =>
            {
                configuration.GetSection(TokensSettings.SectionName).Bind(options);
                options.SecretKey = configuration.GetSection($"{TokensSettings.SectionName}:{nameof(TokensSettings.SecretKey)}").Value;
            }
            );

            services.Configure<GoogleApiSecrets>(options => configuration.GetSection(GoogleApiSecrets.SectionName).Bind(options));
            services.Configure<VkApiSecrets>(options => configuration.GetSection(VkApiSecrets.SectionName).Bind(options));

            var tokenSettings = services.BuildServiceProvider()
                .GetRequiredService<IOptions<TokensSettings>>().Value;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(tokenSettings.SchemeName, options =>
                {
                    options.TokenValidationParameters = JwtGenerator.CreateTokenValidationParameters(tokenSettings.SecretKey, tokenSettings.Issuer, tokenSettings.Audience);

                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = ctx =>
                    //    {
                    //        ctx.Request.Cookies.TryGetValue("access_token", out var accessToken);
                    //        if (!string.IsNullOrEmpty(accessToken))
                    //            ctx.Token = accessToken;
                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            return services;
        }
    }
}
