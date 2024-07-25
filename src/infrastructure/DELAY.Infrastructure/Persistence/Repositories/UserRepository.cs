using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : NamedRepository<User>, IUserStorage
    {
        public Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<UserDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
