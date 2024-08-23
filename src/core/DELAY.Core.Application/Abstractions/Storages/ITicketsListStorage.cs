using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketsListStorage : INamedStorage<TicketsList>
    {
        Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TicketsList>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default);
    }
}
