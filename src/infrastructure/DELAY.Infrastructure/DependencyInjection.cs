using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Mapper;
using DELAY.Infrastructure.Auth;
using DELAY.Infrastructure.Caching;
using DELAY.Infrastructure.Helpers;
using DELAY.Infrastructure.Persistence;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DELAY.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrasturcture(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddServices()
                .AddMapperService()
                .AddPersistenceServices(configurationManager)
                .AddCachingServices(configurationManager)
                .AddAuthServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHelper, PasswordHelper>();

            return services;
        }

        private static IServiceCollection AddMapperService(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

            services.AddMapster();

            services.AddSingleton<IModelMapperService, MapsterMapperService>();

            return services;
        }
    }
}
