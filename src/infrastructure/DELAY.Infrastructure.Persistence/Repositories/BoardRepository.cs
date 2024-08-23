using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class BoardRepository : NamedRepository<BoardEntity, Board>, IBoardStorage
    {
        public BoardRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public Task<int> CountAsync(Guid userId, IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Board>> GetRecordsAsync(Guid userId, IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions paginationOption, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
