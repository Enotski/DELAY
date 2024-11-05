using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Infrastructure.Auth.Services;
using DELAY.Infrastructure.Security;
using DELAY.Presentation.RestAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DELAY.Presentation.RestAPI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавление сервисов API
        /// </summary>
        /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
        /// <returns></returns>
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddCors();
            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.ConfigureAuth(configuration);

            return services;
        }

        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
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

        /// <summary>
        /// Конфигурация <see cref="WebApplicationBuilder"/>
        /// </summary>
        /// <param name="builder"><inheritdoc cref="WebApplicationBuilder"/></param>
        /// <returns></returns>
        public static WebApplicationBuilder ConfigureWebApplication(this WebApplicationBuilder builder)
        {
            builder.AddLogger();

            return builder;
        }
        /// <summary>
        /// Добавление логирования <see cref="WebApplicationBuilder"/>
        /// </summary>
        /// <param name="builder"><inheritdoc cref="WebApplicationBuilder"/></param>
        /// <returns></returns>
        private static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
        {
            string messageTemplate = "{Timestamp:dd.MM.yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

            var logsDir = Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "logs"));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: messageTemplate)
                .WriteTo.File(Path.Combine(logsDir.FullName, "log.txt"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, outputTemplate: messageTemplate)
                .CreateLogger();

            builder.Host.ConfigureLogging(logging =>
            {
                logging.AddSerilog();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .UseSerilog();

            return builder;
        }

        /// <summary>
        /// Конфигурация Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(
            options =>
            {
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "DELAY",
                        Description = "TODO tickets-list app",
                    });
                };

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api_doc.xml"));
            });

            return services;
        }
    }
}
