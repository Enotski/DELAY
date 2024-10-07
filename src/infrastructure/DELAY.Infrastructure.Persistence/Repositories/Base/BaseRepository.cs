using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Context;

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
    }
}
