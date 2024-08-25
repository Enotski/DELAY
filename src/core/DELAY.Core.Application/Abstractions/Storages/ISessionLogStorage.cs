using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ISessionLogStorage : IBaseStorage<SessionLog>
    {
        Task<SessionLog> GetSessionAsync(Guid userId, string ip, string userAgent, CancellationToken cancellationToken = default);

        Task<int> DeleteByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
