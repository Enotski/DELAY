using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface ISessionLogStorage : IBaseStorage<SessionLog>
    {
        Task<SessionLog> GetSession(Guid userId, string ip, string userAgent);
    }
}
