using DELAY.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddPersistence(config);

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager config)
        {
            var dbServerType = config["DbServerType"];
            var connectionString = config.GetConnectionString("DbConnection");

            if (!string.IsNullOrEmpty(dbServerType) && dbServerType.ToUpper() == "POSTGRES")
            {
                services.AddDbContext<DelayContext>(c => c.UseNpgsql(connectionString, serverOptions => serverOptions.CommandTimeout(120)));
            }

            return services;
        }
    }
}
