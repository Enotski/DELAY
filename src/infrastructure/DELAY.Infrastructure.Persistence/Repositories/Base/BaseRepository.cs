using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DELAY.Infrastructure.Persistence.Repositories.Base
{
    internal class BaseRepository<TEntity, TDomain> : IBaseStorage<TDomain>
        where TDomain : class
        where TEntity : class
    {
        protected readonly DelayContext context;

        protected BaseRepository(DelayContext context)
        {
            this.context = context;
        }

        protected DbSet<TEntity> Set { get => context.Set<TEntity>(); }
    }
}
