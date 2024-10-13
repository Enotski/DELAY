using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketsListStorage : INamedStorage<TicketsList>
    {
        Task<TicketsListSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TicketsListSelector>> GetRecordsByBoardAsync(Guid boardId, CancellationToken cancellationToken = default);
    }
}
