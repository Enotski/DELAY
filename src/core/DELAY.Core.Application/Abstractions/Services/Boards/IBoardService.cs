using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Boards
{
    public interface IBoardService : INamedService
    {
        Task<Guid?> CreateBoardAsync(BoardDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Update board
        /// </summary>
        /// <param name="board">Board updated model</param>
        /// <returns></returns>
        Task<int> UpdateBoardAsync(BoardDto board, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetBoardByUserAsync(Guid userId);

        /// <summary>
        /// Get records by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        Task<BoardDto> GetBoardAsync(Guid id, OperationUserInfo triggeredBy);
    }
}
