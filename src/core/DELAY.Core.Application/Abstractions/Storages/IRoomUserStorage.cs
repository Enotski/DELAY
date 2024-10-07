using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IRoomUserStorage : IBaseStorage<ChatRoomUser>
    {
        
        Task<int> CountRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ChatRoomUser>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<int> CountRecordsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ChatRoomUser>> GetRecordsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
    }
}
