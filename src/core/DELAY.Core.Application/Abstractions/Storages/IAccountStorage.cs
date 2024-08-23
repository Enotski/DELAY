using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IAccountStorage
    {
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByPhoneAsync(string phone);
    }
}
