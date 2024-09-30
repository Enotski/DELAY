using DELAY.Core.Application.Contracts.Configuration;
using DELAY.Infrastructure.Caching.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DELAY.Infrastructure.Caching.MemoryCache
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();

        private readonly IMemoryCache _cache;

        private IOptions<CacheServiceConfiguration> _configuration;

        public MemoryCacheService(IOptions<CacheServiceConfiguration> configuration, IMemoryCache cache)
        {
            _configuration = configuration;
            _cache = cache;
        }
        /// <summary>
        /// Удаление значения из кэша
        /// </summary>
        /// <param name="key">Ключ значения</param>
        public bool RemoveData(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return false;

            _cache.Remove(key);
            return true;
        }
        public bool RemoveData(IEnumerable<string> keys)
        {
            bool res = true;
            foreach (var key in keys)
            {
                res = (RemoveData(key) && res ? true : false);
            }
            return false;
        }
        /// <summary>
        /// Сохранение в кэш
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        /// <param name="val">Значение</param>
        public bool SetData<TValue>(string key, TValue val, DateTimeOffset absoluteExpiration)
        {
            if (!string.IsNullOrWhiteSpace(key) && val != null)
            {
                _cache.Set(key, val, BuildCacheEntryOptions(absoluteExpiration));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Сохранение в кэш
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        /// <param name="val">Значение</param>
        public bool SetData<TValue>(string key, TValue val)
        {
            if (!string.IsNullOrWhiteSpace(key) && val != null)
            {
                _cache.Set(key, val, BuildCacheEntryOptions(DateTimeOffset.UtcNow.AddMinutes(_configuration.Value.StorageValueTimeoutMinutes)));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Получение значения из кэша
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        public TValue? GetData<TValue>(string key)
        {
            TValue val;
            if (_cache.TryGetValue(key, out val))
            {
                return val;
            }
            return default;
        }

        public async Task ClearCacheAsync()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                await _resetCacheToken.CancelAsync();
                _resetCacheToken.Dispose();
            }
            _resetCacheToken = new CancellationTokenSource();
        }

        private MemoryCacheEntryOptions BuildCacheEntryOptions(DateTimeOffset absoluteExpiration)
            => new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.Normal)
                    .SetAbsoluteExpiration(absoluteExpiration)
                    .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
    }
}
