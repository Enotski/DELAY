using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    /// <summary>
    /// Storage of tickets lists records
    /// </summary>
    public interface ITicketsListStorage : INamedStorage<TicketsList>
    {
        /// <summary>
        /// Get list record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TicketsListSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get list records by board key
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TicketsListSelector>> GetRecordsByBoardAsync(Guid boardId, CancellationToken cancellationToken = default);
    }
}
