using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IUserStorage : INamedStorage<User>
    {
        Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default);

        Task<PagedData<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default);

        Task<bool> IsUniqueName(string name, Guid? id = null, CancellationToken cancellationToken = default);
        Task<bool> IsUniqueEmail(string email, Guid? id = null, CancellationToken cancellationToken = default);
        Task<bool> IsUniquePhone(string phoneNumber, Guid? id = null, CancellationToken cancellationToken = default);

        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default);

        Task<bool> IsAllowToPerformOperationAsync(RoleType role, Guid triggeredById, CancellationToken cancellationToken = default);
    }
}
