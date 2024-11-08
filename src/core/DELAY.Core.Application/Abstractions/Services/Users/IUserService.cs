using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;

namespace DELAY.Core.Application.Abstractions.Services.Users
{
    /// <summary>
    /// Service of users
    /// </summary>
    public interface IUserService : INamedService
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns></returns>
        Task<Guid> AddAsync(UserDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">User updated model</param>
        /// <returns></returns>
        Task<int> UpdateAsync(UserDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get user record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetAsync(Guid id);

        /// <summary>
        /// Get users records by ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetByTicketAsync(Guid ticketId);

        /// <summary>
        /// Update user's password
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<int> UpdatePasswordAsync(UserPasswordRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get users records by board
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetByBoardAsync(Guid boardId);

        /// <summary>
        /// Get users records by chat
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetByChatRoomAsync(Guid boardId);

        /// <summary>
        /// Get all users as key-name records
        /// </summary>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameDto>> GetKeyNameRecordsAsync(OperationUserInfo triggeredBy);

        /// <summary>
        /// Remove users by keys
        /// </summary>
        /// <param name="id"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<Guid> id, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="searchOptions">Search options</param>
        /// <returns></returns>
        Task<PagedData<UserDto>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination);
    }
}
