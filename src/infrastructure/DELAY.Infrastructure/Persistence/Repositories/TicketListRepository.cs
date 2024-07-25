using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class TicketListRepository : BaseRepository<TicketsList>, ITicketsListStorage
    {
        public Task<int> CountAsync(IEnumerable<SearchOptionsDto> options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetKeyByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<KeyNameDto> GetKeyNameByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, SortOptionsDto sortOption, PaginationOptionsDto pagination, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<KeyNameDto>> GetKeyNamesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNameByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<string>> GetNamesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TicketsListDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto paginationOption, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
