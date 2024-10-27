using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Users
{
    public interface IUserService : INamedService
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task<Guid> AddAsync(UserDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User updated model</param>
        /// <returns></returns>
        Task<int> UpdateAsync(UserDto model, OperationUserInfo triggeredBy);

        Task<UserDto> GetAsync(Guid id);
        Task<IEnumerable<UserDto>> GetByTicketAsync(Guid ticketId);
        Task<int> UpdatePasswordAsync(UserPasswordRequestDto model, OperationUserInfo triggeredBy);
        Task<IEnumerable<UserDto>> GetByBoardAsync(Guid boardId);

        Task<IEnumerable<UserDto>> GetByChatRoomAsync(Guid boardId);
        Task<IEnumerable<KeyNameDto>> GetKeyNameRecordsAsync(OperationUserInfo triggeredBy);
        Task<int> DeleteAsync(IEnumerable<Guid> id, OperationUserInfo triggeredBy);
        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<PagedData<UserDto>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination);
    }
}
