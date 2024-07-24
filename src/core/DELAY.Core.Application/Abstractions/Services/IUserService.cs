using DELAY.Core.Application.Contracts;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services
{
    public interface IUserService : INamedService<User>
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task CreateAsync(User user);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User updated model</param>
        /// <returns></returns>
        Task UpdateAsync(User user);
    }
}
