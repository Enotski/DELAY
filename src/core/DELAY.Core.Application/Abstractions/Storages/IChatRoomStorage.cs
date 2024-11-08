using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    public interface IChatRoomStorage : INamedStorage<ChatRoom>
    {
        Task<Guid> CreateChatAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);
        Task<int> UpdateChatAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);
        Task<ChatRoomSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatMessageSelector>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken = default);
        Task<int> CreateMessageAsync(ChatMessage model, CancellationToken cancellationToken = default);
        Task<bool> IsAllowToPerformOperationAsync(ChatRoomRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default);
    }
}
