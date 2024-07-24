using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketUserStorage : IBaseStorage<TicketUser>
    {
        Task<int> CountRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TicketUserDto>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<int> CountRecordsByRoomAsync(Guid ticketId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TicketUserDto>> GetRecordsByRoomAsync(Guid ticketId, CancellationToken cancellationToken = default);
    }
}
