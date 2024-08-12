using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    //internal class TicketRepository : NamedRepository<Ticket>, ITicketStorage
    //{
    //    public TicketRepository(DelayContext context) : base(context)
    //    {
    //    }

    //    public Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IReadOnlyList<Ticket>> GetAssignedToUserAsync(Guid userId, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IReadOnlyList<Ticket>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
