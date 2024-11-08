namespace DELAY.Core.Application.Contracts.Configuration
{
    /// <summary>
    /// Configurations of caching services
    /// </summary>
    public class CacheServiceConfiguration
    {
        /// <summary>
        /// Name of section in config
        /// </summary>
        public const string SectionName = nameof(CacheServiceConfiguration);

        /// <summary>
        /// Timeout in minutes
        /// </summary>
        public int StorageValueTimeoutMinutes { get; set; }

        /// <summary>
        /// Cache storage variant
        /// </summary>
        /// <value>Строка.</value>
        public string StorageVariant { get; set; } = string.Empty;

        /// <summary>
        /// Storage address
        /// </summary>
        public string StorageURL { get; set; } = string.Empty;
    }
}
