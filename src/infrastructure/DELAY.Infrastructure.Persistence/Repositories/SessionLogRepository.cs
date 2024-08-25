using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class SessionLogRepository : BaseRepository<SessionLogEntity, SessionLog>, ISessionLogStorage
    {
        public SessionLogRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public async Task<SessionLog> GetSessionAsync(Guid userId, string ip, string userAgent, CancellationToken cancellationToken = default)
        {
            var res = await BuildQuery(x => x.UserId == userId && x.IpAddress == ip && x.UserAgent == userAgent).FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<SessionLog>(res);
        }

        public async Task<int> DeleteByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var entities = context.UserSessions.Where(x => x.UserId == userId);

            context.Set<SessionLogEntity>().RemoveRange(entities);

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
