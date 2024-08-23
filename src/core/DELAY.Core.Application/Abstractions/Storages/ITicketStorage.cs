using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketStorage : INamedStorage<Ticket>
    {
        Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Ticket>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Ticket>> GetAssignedToUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
