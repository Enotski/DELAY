using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DELAY.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            var dbServerType = config["DbServerType"];
            string connectionString;
            if (!string.IsNullOrEmpty(dbServerType) && dbServerType.ToUpper() == "POSTGRES")
            {
                connectionString = config.GetConnectionString("PgConnection");
                services.AddDbContext<DelayContext>(c => c.UseNpgsql(connectionString, serverOptions => serverOptions.CommandTimeout(120)));
            }

            services.AddStorages();

            return services;
        }
        private static IServiceCollection AddStorages(this IServiceCollection services)
        { 
            services.AddScoped<IUserStorage, UserRepository>();
            services.AddScoped<IBoardStorage, BoardRepository>();

            return services;
        }
    }
}
