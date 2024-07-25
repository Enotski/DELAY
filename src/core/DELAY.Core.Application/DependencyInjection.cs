using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Mapper;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DELAY.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMapperService();

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
