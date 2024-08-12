using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;
using DELAY.Infrastructure.Builders;
using DELAY.Infrastructure.Extensions;
using DELAY.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DELAY.Infrastructure.Persistence.Repositories.Base
{
    internal abstract class NamedRepository<TEntity, TDomain> : BaseRepository<TEntity, TDomain>, INamedStorage<TDomain>
        where TEntity : class, IKey, IName
        where TDomain : class, IKey, IName
    {
        protected NamedRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        protected IQueryable<TEntity> BuildQueryOrderedByName(Expression<Func<TEntity, bool>> filter = null)
        {
            return BuildQuery(filter, ordered: o => o.OrderBy(x => x.Name));
        }

        protected Expression<Func<TEntity, KeyNamedModel>> KeyNamedSelectorSpecification()
        {
            return x => new KeyNamedModel(x.Id, x.Name);
        }

        public async Task<Guid?> GetKeyByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Guid.Empty;
            }

            name = name.ToUpperTrim();

            return await context.Set<TEntity>()
                .Where(m => m.Name.ToUpper() == name)
                .Select(m => m.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<KeyNamedModel> GetKeyNameByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = ByKeySearchSpecification(id);
            var selector = KeyNamedSelectorSpecification();

            return await context.Set<TEntity>()
                .Where(filter)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            if (!ids.Any())
                return new List<KeyNamedModel>();

            var filter = ByKeysSearchSpecification(ids);
            var selector = KeyNamedSelectorSpecification();

            return await BuildQueryOrderedByName(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<string> GetNameByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = ByKeySearchSpecification(id);

            return await context.Set<TEntity>().Where(filter).Select(m => m.Name).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<string>> GetNamesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var filter = ByKeysSearchSpecification(ids);

            return await BuildQueryOrderedByName(filter)
                .Select(m => m.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedDataModel<KeyNamedModel>> GetKeyNameRecordsAsync(string name, PaginationOptions pagination, CancellationToken cancellationToken)
        {
            var filter = PredicateBuilder.True<TEntity>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.ToUpperTrim();
                filter = filter.And(m => !string.IsNullOrEmpty(m.Name) && m.Name.ToUpper().Contains(name));
            }

            var count = await CountAsync(filter, cancellationToken);

            var records = await BuildQuery(filter, m => m.OrderBy(m => m.Name), pagination)
                .Select(m => new KeyNamedModel(m.Id, m.Name)).ToListAsync(cancellationToken);

            return new PagedDataModel<KeyNamedModel>(count, records);
        }
    }
}
