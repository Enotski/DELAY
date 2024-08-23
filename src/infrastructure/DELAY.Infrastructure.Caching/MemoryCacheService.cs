using DELAY.Core.Application.Abstractions.Services.Common;
using Microsoft.Extensions.Caching.Memory;

namespace DELAY.Infrastructure.Persistence
{
    public class MemoryCacheService : ICachingService
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// Удаление значения из кэша
        /// </summary>
        /// <param name="key">Ключ значения</param>
        public void RemoveValueFromCache(object key)
        {
            _cache.Remove(key);
        }
        /// <summary>
        /// Сохранение в кэш
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        /// <param name="val">Значение</param>
        public void SetValueToCache<TValue>(object key, TValue val, DateTimeOffset absoluteExpiration) where TValue : class
        {
            _cache.Remove(key);
            _cache.Set(key, val, absoluteExpiration);
        }
        /// <summary>
        /// Получение значения из кэша
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        public TValue GetValueFromCache<TValue>(object key) where TValue : class
        {
            TValue val;
            if (_cache.TryGetValue(key, out val))
            {
                return val;
            }
            return null;
        }
    }
}
