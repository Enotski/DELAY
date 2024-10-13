using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketStorage : INamedStorage<Ticket>
    {
        Task<Guid> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task<int> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task<IEnumerable<KeyNameSelector>> GetRecordsByListAsync(Guid listId, CancellationToken cancellationToken = default);
        Task<TicketSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
