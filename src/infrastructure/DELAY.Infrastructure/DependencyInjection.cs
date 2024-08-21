using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Services.Base;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Mapper;
using DELAY.Infrastructure.Cryptography;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DELAY.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrasturcture(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddPersistence(config)
                .AddStorages()
                .AddServices(config)
                .AddMapperService();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager config)
        {
            var dbServerType = config["DbServerType"];
            string connectionString;
            if (!string.IsNullOrEmpty(dbServerType) && dbServerType.ToUpper() == "POSTGRES")
            {
                connectionString = config.GetConnectionString("PgConnection");
                services.AddDbContext<DelayContext>(c => c.UseNpgsql(connectionString, serverOptions => serverOptions.CommandTimeout(120)));
            }

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityConfig>(options => configuration.GetSection(SecurityConfig.SectionName).Bind(options));

            services.AddSingleton<ICryptographyService, CryptographyService>();

            return services;
        }

        private static IServiceCollection AddStorages(this IServiceCollection services)
        {
            services.AddScoped<IUserStorage, UserRepository>();
            services.AddScoped<IBoardStorage, BoardRepository>();

            return services;
        }

        private static IServiceCollection AddMapperService(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddMapster();
            services.AddTransient<IModelMapperService, MapsterMapperService>();
            return services;
        }
    }
}
