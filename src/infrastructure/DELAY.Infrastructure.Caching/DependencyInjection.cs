using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure.Caching
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCachingervices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICachingService, MemoryCacheService>();

            return services;
        }
    }
}
