using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Contracts.Configuration;
using DELAY.Infrastructure.Caching.Abstractions;
using DELAY.Infrastructure.Caching.Common;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Limbo.CacheServices.Common
{
    internal class CacheService : ICacheService
    {
        protected readonly IOptions<CacheServiceConfiguration> _cacheConfigurationSettings;
        protected readonly IRedisCacheService _redisCacheService;
        protected readonly IMemoryCacheService _memoryCacheService;

        public CacheService(IRedisCacheService redisCacheService, IMemoryCacheService memoryCacheService, IOptions<CacheServiceConfiguration> cacheConfigurationSettings)
        {
            _cacheConfigurationSettings = cacheConfigurationSettings;
            _redisCacheService = redisCacheService;
            _memoryCacheService = memoryCacheService;
        }
        /// <summary>
        /// Метод получения закешированных данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public T? GetData<T>(string key)
        {
            return _cacheConfigurationSettings.Value.StorageVariant switch
            {
                StorageVariant.Redis => _redisCacheService.GetData<T>(key),
                StorageVariant.Memory => _memoryCacheService.GetData<T>(key),
                _ => default
            };
        }
        public bool SetData<T>(string key, T val, DateTimeOffset absoluteExpiration)
        {
            return _cacheConfigurationSettings.Value.StorageVariant switch
            {
                StorageVariant.Redis => _redisCacheService.SetData(key, val, absoluteExpiration),
                StorageVariant.Memory => _memoryCacheService.SetData(key, val, absoluteExpiration),
                _ => default

            };
        }

        /// <summary>
        /// Сохранение данных в кеш
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public bool SetData<T>(string key, T value)
        {
            return _cacheConfigurationSettings.Value.StorageVariant switch
            {
                StorageVariant.Redis => _redisCacheService.SetData(key, value),
                StorageVariant.Memory => _memoryCacheService.SetData(key, value),
                _ => default

            };
        }
        /// <summary>
        /// Очистка данных в кеше по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public bool RemoveData(string key)
        {
            return _cacheConfigurationSettings.Value.StorageVariant switch
            {
                StorageVariant.Redis => _redisCacheService.RemoveData(key),
                StorageVariant.Memory => _memoryCacheService.RemoveData(key),
                _ => true
            };
        }
        public bool RemoveData(IEnumerable<string> keys)
        {
            return _cacheConfigurationSettings.Value.StorageVariant switch
            {
                StorageVariant.Redis => _redisCacheService.RemoveData(keys),
                StorageVariant.Memory => _memoryCacheService.RemoveData(keys),
                _ => true
            };
        }
        /// <summary>
        /// Очищает весь кэш
        /// </summary>
        public async Task ClearCacheAsync()
        {
            switch (_cacheConfigurationSettings.Value.StorageVariant)
            {
                case StorageVariant.Redis:
                    await _redisCacheService.ClearCacheAsync();
                    break;
                case StorageVariant.Memory:
                    await _memoryCacheService.ClearCacheAsync();
                    break;
                default:
                    break;
            }
        }
        public string FormatKey(string key, params string[] values)
        {
            if (values is null || values.Length == 0) return $"{key}";
            var fullKey = $"{key}";
            foreach (var value in values)
            {
                fullKey += $"_{value}";
            }
            return fullKey;
        }

        public string FormatKey(string[] values, [CallerMemberName] string key = "")
        {
            return FormatKey(key, values);
        }
    }
}
