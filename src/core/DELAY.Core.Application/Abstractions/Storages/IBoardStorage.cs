using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    /// <summary>
    /// Storage of boards records
    /// </summary>
    public interface IBoardStorage : INamedStorage<Board>
    {
        /// <summary>
        /// Check rights for operation
        /// </summary>
        /// <param name="role"></param>
        /// <param name="triggeredById"></param>
        /// <param name="boardId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsAllowToPerformOperationAsync(BoardRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add new boards
        /// </summary>
        /// <param name="board"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> CreateBoardAsync(Board board, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateBoardAsync(Board board, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get board record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BoardSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get boards records by user key
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get boards records by chat key
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameSelector>> GetKeyNameRecordsByChatAsync(Guid chatId, CancellationToken cancellationToken = default);
    }
}
