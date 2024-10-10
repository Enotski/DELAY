using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IBoardStorage : INamedStorage<Board>
    {
        Task<int> CountAsync(Guid userId, IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Board>> GetRecordsAsync(Guid userId, IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default);

        Task<bool> IsAllowToPerformOperationAsync(BoardRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default);

        Task<Guid> CreateBoardAsync(Board board, CancellationToken cancellationToken = default);

        Task<BoardSelector> GetBoardAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<KeyNameSelector>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
