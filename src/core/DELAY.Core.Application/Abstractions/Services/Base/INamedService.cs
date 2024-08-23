﻿using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Abstractions.Services
{
    /// <summary>
    /// Service for named models
    /// </summary>
    public interface INamedService<T> where T : class, IKey, IName
    {
        /// <summary>
        /// Delete by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="triggeredBy">What invoke operation</param>
        /// <returns></returns>
        Task<int> DeleteAsync(Guid id, string triggeredBy);

        /// <summary>
        /// Delete by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="triggeredBy">What invoke operation</param>
        /// <returns></returns>        
        Task<int> DeleteAsync(IEnumerable<Guid> ids, string triggeredBy);

        /// <summary>
        /// Get records by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Get records by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetRecordsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Get key-name by keys
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids);
    }
}
