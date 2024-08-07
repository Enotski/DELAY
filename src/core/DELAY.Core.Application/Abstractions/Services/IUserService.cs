using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Abstractions.Services
{
    public interface IUserService : INamedService<User>
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task<Guid> AddAsync(User user, string triggeredBy);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User updated model</param>
        /// <returns></returns>
        Task<int> UpdateAsync(User user, string triggeredBy);

        Task<IEnumerable<User>> GetUsersByTicketAsync(Guid ticketId);

        Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId);

        Task<KeyNamedModel> ValidatePermissionToOperation(RoleType roleType, string triggeredByName);

        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<PagedDataModel<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination);
    }
}
