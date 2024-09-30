using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Contracts.Configuration;
using DELAY.Infrastructure.Caching.Abstractions;
using DELAY.Infrastructure.Caching.MemoryCache;
using DELAY.Infrastructure.Caching.RedisCache;
using Limbo.CacheServices.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure.Caching
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.RegisterConfiguration(configurationManager);

            services.AddMemoryCache();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }

        private static IServiceCollection RegisterConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.Configure<CacheServiceConfiguration>(options => configurationManager.GetSection(CacheServiceConfiguration.SectionName).Bind(options));

            return services;
        }
    }
}
