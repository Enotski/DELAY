﻿using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class SessionLogRepository : BaseRepository<SessionLogEntity, SessionLog>, ISessionLogStorage
    {
        public SessionLogRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public Task<SessionLog> GetSessionAsync(Guid userId, string ip, string userAgent, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SessionLog> GetSessionByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
