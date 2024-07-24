using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services
{
    internal interface IBoardService : INamedService<Board>
    {
        /// <summary>
        /// Create board
        /// </summary>
        /// <param name="board">Board model</param>
        /// <returns></returns>
        Task CreateAsync(Board board);

        /// <summary>
        /// Update board
        /// </summary>
        /// <param name="board">Board updated model</param>
        /// <returns></returns>
        Task UpdateAsync(Board board);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<Board>> GetTicketsAssignedToUser(Guid userId);
    }
}
