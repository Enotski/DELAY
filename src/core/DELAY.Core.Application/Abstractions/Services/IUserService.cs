using DELAY.Core.Application.Contracts.Models;

namespace DELAY.Core.Application.Abstractions.Services
{
    public interface IUserService : INamedService<UserDto>
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task CreateAsync(UserDto user);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User updated model</param>
        /// <returns></returns>
        Task UpdateAsync(UserDto user);

        Task<IEnumerable<UserDto>> GetAssigedUsersToTicketAsync(Guid ticketId);

        Task<IEnumerable<UserDto>> GetBoardUsersAsync(Guid boardId);
    }
}
