namespace DELAY.Core.Application.Abstractions.Services.Common
{
    public interface ICacheService
    {
        /// <summary>
        /// Сохранение в кэш
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        /// <param name="val">Значение</param>
        bool SetData<TValue>(string key, TValue val, DateTimeOffset absoluteExpiration);
        bool SetData<TValue>(string key, TValue val);

        /// <summary>
        /// Удаление значения из кэша
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        bool RemoveData(string key);
        bool RemoveData(IEnumerable<string> keys);

        /// <summary>
        /// Получение значения из кэша
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        TValue? GetData<TValue>(string key);

        Task ClearCacheAsync();
    }
}
