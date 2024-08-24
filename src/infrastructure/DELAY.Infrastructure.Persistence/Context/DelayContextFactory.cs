using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.Extensions.Configuration;

namespace DELAY.Infrastructure.Persistence.Context
{
    internal class DelayContextFactory : IDesignTimeDbContextFactory<DelayContext>
    {
        public DelayContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DelayContext>();

            // получаем конфигурацию из файла appsettings.json
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("persistence.settings.json")
                .Build();

            var dbServerType = config["DbServerType"];
            string connectionString;

            if (!string.IsNullOrEmpty(dbServerType) && dbServerType.ToUpper() == "POSTGRES")
            {
                connectionString = config.GetConnectionString("PgConnection");
                optionsBuilder.UseNpgsql(connectionString, serverOptions => serverOptions.CommandTimeout(120));
            }

            return new DelayContext(optionsBuilder.Options);
        }
    }
}
