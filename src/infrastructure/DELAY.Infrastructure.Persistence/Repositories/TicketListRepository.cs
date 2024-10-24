using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class TicketsListRepository : NamedRepository<TicketsListEntity, TicketsList>, ITicketsListStorage
    {
        public TicketsListRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public async Task<TicketsListSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Expression<Func<TicketsListEntity, TicketsListSelector>> selector = x
                => new TicketsListSelector(x.Id, x.Name, x.BoardId, x.Tickets.Select(xx => new KeyNameSelector(xx.Id, xx.Name)));

            var filter = ByKeySearchSpecification(id);

            return await BuildQuery(filter)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TicketsListSelector>> GetRecordsByBoardAsync(Guid boardId, CancellationToken cancellationToken = default)
        {
            Expression<Func<TicketsListEntity, TicketsListSelector>> selector = x
                => new TicketsListSelector(x.Id, x.Name, x.BoardId, x.Tickets.Select(xx => new KeyNameSelector(xx.Id, xx.Name)));

            Expression<Func<TicketsListEntity, bool>> filter = x
                => x.BoardId == boardId;

            return await BuildQuery(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }
    }
}
