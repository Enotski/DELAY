using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IRoomUserStorage : IBaseStorage<RoomUser>
    {
        Task<int> CountRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RoomUserDto>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<int> CountRecordsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RoomUserDto>> GetRecordsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
    }
}
