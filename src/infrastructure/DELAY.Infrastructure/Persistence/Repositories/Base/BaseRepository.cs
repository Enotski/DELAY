using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace DELAY.Infrastructure.Persistence.Repositories.Base
{
    internal class BaseRepository<TEntity, TDomain> : IBaseStorage<TDomain> 
        where TDomain : class, IKey
        where TEntity : class, IKey
    {
        protected readonly DelayContext context;

        protected readonly IModelMapperService mapper;

        protected BaseRepository(DelayContext context, IModelMapperService mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        /// <summary>
        /// Спецификация поиска ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns></returns>
        protected Expression<Func<TEntity, bool>> ByKeySearchSpecification(Guid id)
        {
            return x => x.Id == id;
        }

        /// <summary>
        /// Спецификация поиска ключам
        /// </summary>
        /// <param name="ids">Ключи записей</param>
        /// <returns></returns>
        protected Expression<Func<TEntity, bool>> ByKeysSearchSpecification(IEnumerable<Guid> ids)
        {
            return x => ids.Contains(x.Id);
        }

        /// <summary>
        /// Удобное создание запроса на выборку сущностей
        /// </summary>
        /// <param name="filter">Фильтр для выборки</param>
        /// <returns></returns>
        protected IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ordered = null,
             PaginationOptions pagination = null, IEnumerable<Expression<Func<TEntity, object>>> includes = null)
        {

            IQueryable<TEntity> query = context.Set<TEntity>();

            if (includes is not null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (ordered is not null)
            {
                query = ordered(query);
            }

            if (pagination is not null)
            {
                if (pagination.Skip >= 0)
                {
                    query = query.Skip(pagination.Skip.Value);
                }

                if (pagination.Take > 0)
                {
                    query = query.Take(pagination.Take.Value);
                }
            }
            return query;
        }

        /// <summary>
        /// Получение кол-ва сущностей
        /// </summary>
        /// <param name="filter">Параметры фильтрации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        protected async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await BuildQuery(filter).CountAsync(cancellationToken);
        }

        /// <summary>
        /// Удаление сущностей
        /// </summary>
        /// <param name="predicate">Параметры фильтрации</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().Where(predicate).DeleteAsync(cancellationToken);
        }

        public async Task<Guid?> AddAsync(TDomain model, CancellationToken cancellationToken = default)
        {
            var entity = mapper.Map<TEntity>(model);

            context.Set<TEntity>().Add(entity);

            var res = await context.SaveChangesAsync(cancellationToken);

            if (res == 1) 
                return entity.Id;
            else 
                return null;
        }

        public async Task<int> AddAsync(IEnumerable<TDomain> models, CancellationToken cancellationToken = default)
        {
            if (!models.Any())
            {
                return 0;
            }

            var entities = mapper.Map<IEnumerable<TEntity>>(models);

            context.Set<TEntity>().AddRange(entities);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(TDomain model, CancellationToken cancellationToken = default)
        {
            var entity = mapper.Map<TEntity>(model);

            context.Set<TEntity>().Remove(entity);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);

            if (entity is not null)
            {
                return await DeleteAsync(entity, cancellationToken);
            }

            return 0;
        }

        public async Task<int> DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            if (!ids.Any())
            {
                return 0;
            }

            int page = -1, rows = 512;
            int deleted = 0;

            IEnumerable<Guid> query = null!;
            do
            {
                query = ids.Skip(++page * rows).Take(rows);

                deleted += await context.Set<TEntity>()
                    .Where(x => query.Contains(x.Id)).DeleteAsync(x => x.BatchSize = rows, cancellationToken);

            } while (query.Any());

            return deleted;
        }

        public async Task<int> DeleteAsync(IEnumerable<TDomain> models, CancellationToken cancellationToken = default)
        {
            if (!models.Any())
            {
                return 0;
            }

            var entities = mapper.Map<IEnumerable<TEntity>>(models);

            foreach (var entity in entities)
            {
                context.Set<TEntity>().Remove(entity);
            }

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TDomain> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = ByKeySearchSpecification(id);

            var result = await context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);

            return mapper.Map<TDomain>(result);
        }

        public async Task<IReadOnlyList<TDomain>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var filter = ByKeysSearchSpecification(ids);

            var result = await BuildQuery(filter).ToListAsync(cancellationToken);

            return mapper.Map<IReadOnlyList<TDomain>>(result);
        }

        public async Task<int> UpdateAsync(TDomain model, CancellationToken cancellationToken = default)
        {
            var entity = mapper.Map<TEntity>(model);

            context.Entry(entity).State = EntityState.Modified;

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(IEnumerable<TDomain> entities, CancellationToken cancellationToken = default)
        {
            if (!entities.Any())
            {
                return 0;
            }

            foreach (var entity in entities)
            {
                context.Entry(entity).State = EntityState.Modified;
            }

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
