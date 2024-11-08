using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Builders;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class RoomRepository : NamedRepository<ChatRoomEntity, ChatRoom>, IChatRoomStorage
    {
        public RoomRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> CreateChatAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default)
        {

            return await AddAsync(chatRoom, (entity, dbContext) =>
            {
                if (chatRoom.RoomUsers.Any())
                {
                    var usersEntities = chatRoom.RoomUsers.Select(x => new ChatRoomUserEntity(entity.Id, x.User.Id, x.UserRole));
                    entity.ChatRoomUsers = null;
                    dbContext.Set<ChatRoomUserEntity>().AddRange(usersEntities);

                    var boardsEntities = chatRoom.Boards.Select(x => new BoardChatRoomEntity(x.Id, entity.Id));
                    entity.BoardChatRooms = null;
                    dbContext.Set<BoardChatRoomEntity>().AddRange(boardsEntities);
                }
            }, cancellationToken);
        }

        public async Task<int> UpdateChatAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(chatRoom, (entity, dbContext) =>
            {
                entity.ChatRoomUsers = null;
                entity.BoardChatRooms = null;

                var toRemoveUsers = dbContext.Set<ChatRoomUserEntity>().Where(x => x.ChatRoomId == chatRoom.Id);
                dbContext.Set<ChatRoomUserEntity>().RemoveRange(toRemoveUsers);

                if (chatRoom.RoomUsers.Any())
                {
                    var toAdd = chatRoom.RoomUsers.Select(x => new ChatRoomUserEntity(chatRoom.Id, x.User.Id, x.UserRole));
                    dbContext.Set<ChatRoomUserEntity>().AddRange(toAdd);
                }

                var toRemoveBoards = dbContext.Set<BoardChatRoomEntity>().Where(x => x.BoardId == chatRoom.Id);
                dbContext.Set<BoardChatRoomEntity>().RemoveRange(toRemoveBoards);

                if (chatRoom.Boards.Any())
                {
                    var toAdd = chatRoom.Boards.Select(x => new BoardChatRoomEntity(x.Id, chatRoom.Id));
                    dbContext.Set<BoardChatRoomEntity>().AddRange(toAdd);
                }
            }, cancellationToken);
        }

        public async Task<ChatRoomSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Expression<Func<ChatRoomEntity, ChatRoomSelector>> selector = x
                => new ChatRoomSelector(x.Id, x.Name, x.ChatType, x.BoardChatRooms.Select(xx => new KeyNameSelector(xx.Board.Id, xx.Board.Name)), x.ChatRoomUsers.Select(xx => new ChatRoomUserSelector(new KeyNameSelector(xx.UserId, xx.User.Name), xx.UserRole)));

            var filter = ByKeySearchSpecification(id);

            return await BuildQuery(filter)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            Expression<Func<ChatRoomEntity, KeyNameSelector>> selector = x => new KeyNameSelector(x.Id, x.Name);

            Expression<Func<ChatRoomEntity, bool>> filter = x => x.ChatRoomUsers.Any(xx => xx.UserId == userId);

            return await BuildQuery(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ChatMessageSelector>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken = default)
        {
            Expression<Func<ChatMessageEntity, ChatMessageSelector>> selector = x => new ChatMessageSelector(x.ChatId, x.Time.ToLocalTime(), x.Author, x.Text);

            Expression<Func<ChatMessageEntity, bool>> filter = x => x.ChatId == chatId;

            return await context.Set<ChatMessageEntity>()
                .Where(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CreateMessageAsync(ChatMessage model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<ChatMessageEntity>(model);

            context.Set<ChatMessageEntity>().Add(entity);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsAllowToPerformOperationAsync(ChatRoomRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<ChatRoomUserEntity>(x => x.UserId == triggeredById && x.ChatRoomId == boardId && x.UserRole >= role);

            return await context.Set<ChatRoomUserEntity>().AnyAsync(filter, cancellationToken);
        }
    }
}
