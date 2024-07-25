using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Abstractions.Storages.Base
{
    public interface INamedStorage<T> : IBaseStorage<T> where T : class, IKey, IName
    {
        /// <summary>
        /// Get name by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<string> GetNameByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get names by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<IReadOnlyList<string>> GetNamesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get key-name record
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<KeyNameDto> GetKeyNameByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get key-name records
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetKeyNamesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get key by record name
        /// </summary>
        /// <param name="name">Record name</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<Guid> GetKeyByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get key-name records by search term
        /// </summary>
        /// <param name="term">Search term</param>
        /// <param name="paginationOption"><inheritdoc cref="PaginationOptionsDto"/></param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, SortOptionsDto sortOption, PaginationOptionsDto pagination, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get key-name records by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }
}
