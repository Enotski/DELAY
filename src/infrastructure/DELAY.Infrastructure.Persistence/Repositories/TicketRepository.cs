using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class TicketRepository : NamedRepository<TicketEntity, Ticket>, ITicketStorage
    {
        public TicketRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            return await AddAsync(ticket, (entity, dbContext) =>
            {
                if (ticket.Users.Any())
                {
                    var entities = ticket.Users.Select(x => new TicketUserEntity(entity.Id, x.Id));
                    entity.Users = null;
                    dbContext.Set<TicketUserEntity>().AddRange(entities);
                }
            }, cancellationToken);
        }

        public async Task<int> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(ticket, (entity, dbContext) =>
            {
                var toRemove = dbContext.Set<TicketUserEntity>().Where(x => x.TicketId == ticket.Id);
                dbContext.Set<TicketUserEntity>().RemoveRange(toRemove);

                if (ticket.Users.Any())
                {
                    var entities = ticket.Users.Select(x => new TicketUserEntity(ticket.Id, x.Id));
                    entity.Users = null;
                    dbContext.Set<TicketUserEntity>().AddRange(entities);
                }
            }, cancellationToken);
        }

        public async Task<IEnumerable<KeyNameSelector>> GetRecordsByListAsync(Guid listId, CancellationToken cancellationToken = default)
        {
            Expression<Func<TicketEntity, KeyNameSelector>> selector = x
                => new KeyNameSelector(x.Id, x.Name);

            Expression<Func<TicketEntity, bool>> filter = x
                => x.TicketListId == listId;

            return await BuildQuery(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<TicketSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Expression<Func<TicketEntity, TicketSelector>> selector = x
                => new TicketSelector(x.Id, x.Name, x.IsDone, x.Description, x.ChangedDate, x.CreateDate, x.ChangedBy, x.CreatedBy,
                x.Users.Select(xx => new KeyNameSelector(xx.UserId, xx.User.Name)));

            var filter = ByKeySearchSpecification(id);

            return await BuildQuery(filter)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            Expression<Func<TicketEntity, KeyNameSelector>> selector = x
                => new KeyNameSelector(x.Id, x.Name);

            Expression<Func<TicketEntity, bool>> filter = x
                => x.Users.Any(xx => xx.UserId == userId);

            return await BuildQuery(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<KeyNameSelector>> GetUsersAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            Expression<Func<TicketUserEntity, KeyNameSelector>> selector = x
                => new KeyNameSelector(x.UserId, x.User.Name);

            Expression<Func<TicketUserEntity, bool>> filter = x
                => x.TicketId == ticketId;

            return await context.Set<TicketUserEntity>()
                .Where(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }
    }
}
