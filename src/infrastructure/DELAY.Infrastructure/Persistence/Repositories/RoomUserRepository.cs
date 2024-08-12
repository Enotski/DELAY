using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    //internal class RoomUserRepository : BaseRepository<RoomUser>, IRoomUserStorage
    //{
    //    protected RoomUserRepository(DelayContext context) : base(context)
    //    {
    //    }

    //    public Task<int> CountRecordsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> CountRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IReadOnlyList<RoomUser>> GetRecordsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IReadOnlyList<RoomUser>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
