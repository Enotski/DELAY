using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class TicketUserRepository : BaseRepository<TicketUserDto>, ITicketUserStorage
    {
        public Task<int> CountRecordsByRoomAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TicketUserDto>> GetRecordsByRoomAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TicketUserDto>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
