using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Core.Application.Contracts.Configuration
{
    public class CacheServiceConfiguration
    {
        /// <summary>
        /// Название раздела.
        /// </summary>
        public const string SectionName = nameof(CacheServiceConfiguration);

        public int StorageValueTimeoutMinutes { get; set; }
        /// <summary>
        /// Вариант кеширования.
        /// </summary>
        /// <value>Строка.</value>
        public string StorageVariant { get; set; } = string.Empty;

        public string StorageURL { get; set; } = string.Empty;
    }
}
