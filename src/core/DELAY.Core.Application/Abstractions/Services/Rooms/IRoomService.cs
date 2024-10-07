using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Rooms
{
    internal interface IRoomService : INamedService<ChatRoom>
    {
        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="room">ChatRoom model</param>
        /// <returns></returns>
        Task CreateAsync(ChatRoom user);

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="room">ChatRoom updated model</param>
        /// <returns></returns>
        Task UpdateAsync(ChatRoom room);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<ChatRoom>> GetTicketsAssignedToUser(Guid userId);
    }
}
