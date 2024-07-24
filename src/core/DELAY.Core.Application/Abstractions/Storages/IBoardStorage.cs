using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IBoardStorage : INamedStorage<Board>
    {
        Task<int> CountAsync(Guid userId, IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<BoardDto>> GetRecordsAsync(Guid userId, IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default);
    }
}
