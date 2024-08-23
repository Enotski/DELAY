using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Users
{
    public interface IUserService : INamedService<User>
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task<Guid?> AddAsync(User user, string triggeredBy);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User updated model</param>
        /// <returns></returns>
        Task<int> UpdateAsync(User user, string triggeredBy);

        Task<IEnumerable<User>> GetUsersByTicketAsync(Guid ticketId);

        Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId);

        /// <summary>
        /// Get records by search options
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<PagedData<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination);
    }
}
