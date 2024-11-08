using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    /// <summary>
    /// Storage of tickets records
    /// </summary>
    public interface ITicketStorage : INamedStorage<Ticket>
    {
        /// <summary>
        /// Save new ticket record
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update ticket record
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get tickets records by list key
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameSelector>> GetRecordsByListAsync(Guid listId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get ticket record by key
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TicketSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get tickets records by user key
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get users records by ticket key
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameSelector>> GetUsersAsync(Guid ticketId, CancellationToken cancellationToken = default);
    }
}
