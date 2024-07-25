using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class TicketRepository : NamedRepository<Ticket>, ITicketStorage
    {
        public Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TicketDto>> GetAssignedToUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TicketDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
