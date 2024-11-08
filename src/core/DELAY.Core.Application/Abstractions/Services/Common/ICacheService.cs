namespace DELAY.Core.Application.Abstractions.Services.Common
{
    /// <summary>
    /// Common cahce service
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Save to cache
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="val">Value</param>
        /// <param name="absoluteExpiration">Expiration</param>
        bool SetData<TValue>(string key, TValue val, DateTimeOffset absoluteExpiration);

        /// <summary>
        /// Save to cache
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="val">Value</param>
        /// <returns></returns>
        bool SetData<TValue>(string key, TValue val);

        /// <summary>
        /// Remove value
        /// </summary>
        /// <param name="key">Value key</param>
        bool RemoveData(string key);

        /// <summary>
        /// Remove values
        /// </summary>
        /// <param name="keys">Values keys</param>
        /// <returns></returns>
        bool RemoveData(IEnumerable<string> keys);

        /// <summary>
        /// Get value
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        TValue? GetData<TValue>(string key);

        /// <summary>
        /// Cleat storage
        /// </summary>
        /// <returns></returns>
        Task ClearCacheAsync();
    }
}
