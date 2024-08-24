using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Mapper;
using DELAY.Infrastructure.Helpers;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DELAY.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrasturctureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices(config)
                .AddMapperService();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
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
