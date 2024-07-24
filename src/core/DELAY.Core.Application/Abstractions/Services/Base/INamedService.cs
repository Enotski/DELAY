using DELAY.Core.Application.Contracts;
using DELAY.Core.Domain.Interfaces;
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
        Task<T> GetRecordAsync(Guid id);

        /// <summary>
        /// Get key-name record by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        Task<KeyNamedModel> GetKeyNameRecordAsync(Guid id);

        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetRecordsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Get key-name by search options
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetRecordsAsync(SearchOptionsDto options);

        /// <summary>
        /// Get key-name by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(SearchOptionsDto options);

        /// <summary>
        /// Get records by pagination options
        /// </summary>
        /// <param name="options">Pagination options</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetRecordsAsync(PaginationOptionsDto options);

        /// <summary>
        /// Get key-name by pagination options
        /// </summary>
        /// <param name="options">Pagination options</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(PaginationOptionsDto options);

        /// <summary>
        /// Get records
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetRecordsAsync();

        /// <summary>
        /// Get key-name
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync();
    }
}
