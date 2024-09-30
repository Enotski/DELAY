using StackExchange.Redis;

namespace DELAY.Infrastructure.Caching.RedisCache
{
    public class RedisConnectionHelper
    {
        public RedisConnectionHelper(string redisUrl)
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                var options = ConfigurationOptions.Parse(redisUrl);
                options.AllowAdmin = true;
                return ConnectionMultiplexer.Connect(options);
            });

            Connection = lazyConnection.Value;
        }

        private static Lazy<ConnectionMultiplexer>? lazyConnection;
        public ConnectionMultiplexer Connection { get; private set; }
    }
}
