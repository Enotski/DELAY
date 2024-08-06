using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace DELAY.Presentation.RestAPI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавление сервисов API
        /// </summary>
        /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
        /// <returns></returns>
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwagger();

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

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }
    }
}
