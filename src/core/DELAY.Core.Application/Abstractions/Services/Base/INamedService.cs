using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Interfaces;

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
        Task DeleteAsync(Guid id, string triggeredBy);

        /// <summary>
        /// Delete by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="triggeredBy">What invoke operation</param>
        /// <returns></returns>        
        Task DeleteAsync(IEnumerable<Guid> ids, string triggeredBy);

        /// <summary>
        /// Get records by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        Task<T> GetRecordAsync(Guid id);

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
        Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto pagination);

        /// <summary>
        /// Get key-name by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, SortOptionsDto sortOption, PaginationOptionsDto pagination);
    }
}
