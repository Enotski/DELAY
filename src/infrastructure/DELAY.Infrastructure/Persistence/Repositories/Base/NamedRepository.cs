using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Repositories.Base
{
    internal class NamedRepository<T> : BaseRepository<T>, INamedStorage<T>
        where T : class, IKey, IName
    {
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
    }
}
