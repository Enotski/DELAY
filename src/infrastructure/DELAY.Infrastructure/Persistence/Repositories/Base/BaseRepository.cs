using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Repositories.Base
{
    internal class BaseRepository<T> : IBaseStorage<T> where T : class, IKey
    {
        public Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public T Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<T> Get(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
