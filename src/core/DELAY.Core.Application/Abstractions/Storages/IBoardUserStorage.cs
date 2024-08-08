using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IBoardUserStorage : IBaseStorage<BoardUser>
    {
        Task<int> CountRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<BoardUser>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
