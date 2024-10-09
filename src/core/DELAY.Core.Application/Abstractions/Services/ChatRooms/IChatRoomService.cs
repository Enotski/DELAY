using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Rooms
{
    internal interface IChatRoomService : INamedService<ChatRoom>
    {
        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="room">ChatRoom model</param>
        /// <returns></returns>
        Task CreateAsync(ChatRoomDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="room">ChatRoom updated model</param>
        /// <returns></returns>
        Task UpdateAsync(ChatRoomDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetByUserAsync(Guid userId);
    }
}
