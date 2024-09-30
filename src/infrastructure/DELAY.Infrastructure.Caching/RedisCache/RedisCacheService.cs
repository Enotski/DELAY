using DELAY.Core.Application.Contracts.Configuration;
using DELAY.Infrastructure.Caching.Abstractions;
using DELAY.Infrastructure.Caching.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DELAY.Infrastructure.Caching.RedisCache
{
    internal class RedisCacheService : IRedisCacheService
    {

        private readonly IOptions<CacheServiceConfiguration> _cacheConfigurationSettings;

        private IDatabase? _db;

        private IServer? _server;

        public RedisCacheService(IOptions<CacheServiceConfiguration> cacheConfigurationSettings)
        {
            _cacheConfigurationSettings = cacheConfigurationSettings;

            if (cacheConfigurationSettings.Value.StorageVariant == StorageVariant.Redis &&
                !string.IsNullOrEmpty(cacheConfigurationSettings.Value.StorageURL))
            {
                ConfigureRedis(cacheConfigurationSettings.Value.StorageURL);
            }
        }
        private void ConfigureRedis(string redisUrl)
        {
            var connHelper = new RedisConnectionHelper(redisUrl);

            _server = connHelper.Connection.GetServer(redisUrl);
            _db = connHelper.Connection.GetDatabase();
        }

        public T? GetData<T>(string key)
        {
            if (_db == null)
                return default;

            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
                return JsonConvert.DeserializeObject<T>(value!, new JsonSerializerSettings())!;

            return default;
        }

        public bool SetData<TValue>(string key, TValue val, DateTimeOffset absoluteExpiration)
        {
            if (_db == null)
                return false;

            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(val), absoluteExpiration - DateTime.UtcNow);
            return isSet;
        }

        public bool SetData<T>(string key, T val)
        {
            if (_db == null)
                return false;

            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(val), new TimeSpan(0, _cacheConfigurationSettings.Value.StorageValueTimeoutMinutes, 0));
            return isSet;
        }

        public bool RemoveData(string key)
        {
            if (_db == null)
                return false;

            return _db.KeyDelete(key);
        }
        public bool RemoveData(IEnumerable<string> keys)
        {
            bool res = true;
            foreach (var key in keys)
            {
                res = RemoveData(key) && res ? true : false;
            }
            return false;
        }

        public async Task ClearCacheAsync()
        {
            if (_db != null && _server != null)
            {
                await _server.FlushDatabaseAsync(_db.Database);
            }
        }
    }
}
