using DELAY.Core.Application.Contracts;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services
{
    internal interface IRoomService : INamedService<Room>
    {
        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="room">Room model</param>
        /// <returns></returns>
        Task CreateAsync(Room user);

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="room">Room updated model</param>
        /// <returns></returns>
        Task UpdateAsync(Room room);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<Room>> GetTicketsAssignedToUser(Guid userId);
    }
}
