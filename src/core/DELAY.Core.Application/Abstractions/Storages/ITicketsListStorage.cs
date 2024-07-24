using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketsListStorage : INamedStorage<TicketsList>
    {
        Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TicketsListDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default);
    }
}
