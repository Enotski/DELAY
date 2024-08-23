using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Abstractions.Storages.Base
{
    /// <summary>
    /// Base storage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseStorage<T> where T : class, IKey
    {
        /// <summary>
        /// Get record by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get records by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add record
        /// </summary>
        /// <param name="model">Record</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<Guid> AddAsync(T model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add records
        /// </summary>
        /// <param name="models">Records</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> AddAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="model">Record</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update records
        /// </summary>
        /// <param name="models">Records</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="model">Key</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> DeleteAsync(T model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete record by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete records by keys
        /// </summary>
        /// <param name="ids">Keys</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete records
        /// </summary>
        /// <param name="models">Records</param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);
    }
}
