namespace DELAY.Infrastructure.Caching.Common
{
    internal struct StorageVariant
    {
        /// <summary>
        /// Кэширование с помощью Memory
        /// </summary>
        public const string Memory = nameof(Memory);
        /// <summary>
        /// Кэширование с помощью Redis
        /// </summary>
        public const string Redis = nameof(Redis);
    }
}
