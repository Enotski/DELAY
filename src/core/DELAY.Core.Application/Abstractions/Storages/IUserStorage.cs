using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    /// <summary>
    /// Storage of users records
    /// </summary>
    public interface IUserStorage : INamedStorage<User>
    {
        /// <summary>
        /// Get count of filtered records
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get records by filters and sorts
        /// </summary>
        /// <param name="searchOptions"></param>
        /// <param name="sortOptions"></param>
        /// <param name="paginationOption"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedData<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get users of ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get users of board
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unique name check
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsUniqueName(string name, Guid? id = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unique email check
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsUniqueEmail(string email, Guid? id = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unique phone check
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsUniquePhone(string phoneNumber, Guid? id = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get record by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get record by phone
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default);

        /// <summary>
        /// Chek rights for operation
        /// </summary>
        /// <param name="role"></param>
        /// <param name="triggeredById"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsAllowToPerformOperationAsync(RoleType role, Guid triggeredById, CancellationToken cancellationToken = default);
    }
}
