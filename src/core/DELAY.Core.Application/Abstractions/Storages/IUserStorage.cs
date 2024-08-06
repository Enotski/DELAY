using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IUserStorage : INamedStorage<User>
    {
        Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<User>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default);

        Task<bool> IsUnique(string name, string email, string phoneNumber, Guid? id = null, CancellationToken cancellationToken = default);

        Task<KeyNamedModel> PermissionToPerformOperationAsync(RoleType role, string triggeredBy);
    }
}
