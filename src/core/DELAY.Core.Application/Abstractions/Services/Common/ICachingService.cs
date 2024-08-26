namespace DELAY.Core.Application.Abstractions.Services.Common
{
    public interface ICachingService
    {
        /// <summary>
        /// Сохранение в кэш
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        /// <param name="val">Значение</param>
        public void SetValueToCache<TValue>(object key, TValue val, DateTimeOffset absoluteExpiration) where TValue : class;
        public void SetValueToCache<TValue>(object key, TValue val) where TValue : class;

        /// <summary>
        /// Удаление значения из кэша
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        public void RemoveValueFromCache(object key);

        /// <summary>
        /// Получение значения из кэша
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="key">Ключ значения</param>
        public TValue GetValueFromCache<TValue>(object key) where TValue : class;
    }
}
