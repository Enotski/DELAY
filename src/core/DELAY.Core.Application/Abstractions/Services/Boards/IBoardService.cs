using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Abstractions.Services.Boards
{
    /// TODO Should be splitted....
    /// <summary>
    /// Service of boards and tickets
    /// </summary>
    public interface IBoardService : INamedService
    {
        /// <summary>
        /// Create board
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get board by chat key
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetBoardByChatAsync(Guid chatId, OperationUserInfo triggeredBy);



        /// <summary>
        /// Get tickets list model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<TicketsListDto> GetTicketsListAsync(TicketsListRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get tickets lists of board
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<IEnumerable<TicketsListDto>> GetTicketsListByBoardAsync(Guid boardId, OperationUserInfo triggeredBy);

        /// <summary>
        /// Create new list for board
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<Guid?> CreateTicketsListAsync(TicketsListDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Update list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<int> UpdateTicketsListAsync(TicketsListDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Remove list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<int> DeleteTicketsListAsync(TicketsListRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get tickets
        /// </summary>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<IEnumerable<TicketsListDto>> GetTicketsByUserAsync(OperationUserInfo triggeredBy);


        /// <summary>
        /// Get tickets of list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameDto>> GetTicketsByListAsync(TicketsByListRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Add ticket
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<UpdateTicketResultDto> CreateTicketAsync(TicketDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Edit ticket
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<UpdateTicketResultDto> UpdateTicketAsync(TicketDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get ticket model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<TicketDto> GetTicketAsync(TicketRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Remove ticket
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<UpdateTicketResultDto> DeleteTicketAsync(TicketRequestDto model, OperationUserInfo triggeredBy);
    }
}
