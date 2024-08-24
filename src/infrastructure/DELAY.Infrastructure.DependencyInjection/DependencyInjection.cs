using DELAY.Infrastructure.Auth;
using DELAY.Infrastructure.Caching;
using DELAY.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddInfrasturctureServices(config)
                .AddPersistenceServices(config)
                .AddCachingServices()
                .AddAuthServices(config);

            return services;
        }
    }
}
