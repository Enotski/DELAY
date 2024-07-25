using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IUserStorage : INamedStorage<User>
    {
        Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<UserDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserDto>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserDto>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default);
    }
}
