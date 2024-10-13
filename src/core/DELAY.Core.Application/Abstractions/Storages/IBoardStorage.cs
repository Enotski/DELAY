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
        Task<bool> IsAllowToPerformOperationAsync(BoardRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default);

        Task<Guid> CreateBoardAsync(Board board, CancellationToken cancellationToken = default);
        Task<int> UpdateBoardAsync(Board board, CancellationToken cancellationToken = default);

        Task<BoardSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<KeyNameSelector>> GetKeyNameRecordsByChatAsync(Guid chatId, CancellationToken cancellationToken = default);
    }
}
