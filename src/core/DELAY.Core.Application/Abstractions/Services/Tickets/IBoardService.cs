using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Tickets
{
    public interface IBoardService : INamedService<Board>
    {
        /// <summary>
        /// Create board
        /// </summary>
        /// <param name="board">Board model</param>
        /// <returns></returns>
        Task<Guid?> AddAsync(Board board, string triggeredBy);

        /// <summary>
        /// Update board
        /// </summary>
        /// <param name="board">Board updated model</param>
        /// <returns></returns>
        Task<int> UpdateAsync(Board board, string triggeredBy);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<Board>> GetBoardsAssignedToUser(Guid userId);
    }
}
