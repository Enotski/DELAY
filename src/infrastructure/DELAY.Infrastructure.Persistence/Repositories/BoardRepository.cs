using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Builders;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class BoardRepository : NamedRepository<BoardEntity, Board>, IBoardStorage
    {
        public BoardRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> CreateBoardAsync(Board board, CancellationToken cancellationToken = default)
        {
            return await AddAsync(board, (id, dbContext) => {
                if (board.BoardUsers.Any())
                {
                    var entities = board.BoardUsers.Select(x => new BoardUserEntity(id, x.User.Id, x.UserRole));

                    dbContext.Set<BoardUserEntity>().AddRange(entities);
                }
            }, cancellationToken);
        }

        public async Task<int> UpdateBoardAsync(Board board, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(board, (id, dbContext) => {
                var toRemove = dbContext.Set<BoardUserEntity>().Where(x => x.BoardId == board.Id);
                dbContext.Set<BoardUserEntity>().RemoveRange(toRemove);

                if (board.BoardUsers.Any())
                {
                    var toAdd = board.BoardUsers.Select(x => new BoardUserEntity(id, x.User.Id, x.UserRole));
                    dbContext.Set<BoardUserEntity>().AddRange(toAdd);
                }
            }, cancellationToken);
        }

        public async Task<BoardSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Expression<Func<BoardEntity, BoardSelector>> selector = x
                => new BoardSelector(x.Id, x.Name, x.Description, x.IsPublic, 
                x.BoardUsers.Select(xx => new BoardUserSelector(new KeyNameSelector(xx.UserId, xx.User.Name), xx.UserRole)));

            var filter = ByKeySearchSpecification(id);

            return await BuildQuery(filter)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            Expression<Func<BoardEntity, KeyNameSelector>> selector = x
                => new KeyNameSelector(x.Id, x.Name);

            Expression<Func<BoardEntity, bool>> filter = x 
                => x.IsPublic || x.BoardUsers.Any(xx => xx.UserId == userId);

            return await BuildQuery(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<KeyNameSelector>> GetKeyNameRecordsByChatAsync(Guid chatId, CancellationToken cancellationToken = default)
        {
            Expression<Func<BoardEntity, KeyNameSelector>> selector = x
                => new KeyNameSelector(x.Id, x.Name);

            Expression<Func<BoardEntity, bool>> filter = x
                => x.BoardChatRooms.Any(xx => xx.ChatRoomId == chatId);

            return await BuildQuery(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsAllowToPerformOperationAsync(BoardRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<BoardUserEntity>(x => x.UserId == triggeredById && x.BoardId == boardId && x.UserRole >= role);

            return await context.Set<BoardUserEntity>().AnyAsync(filter, cancellationToken);
        }
    }
}
