using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ITicketStorage : INamedStorage<Ticket>
    {
        Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TicketDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TicketDto>> GetAssignedToUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
