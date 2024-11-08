using DELAY.Core.Application.Abstractions.Storages.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Storages
{
    /// TODO spilt storage....
    /// <summary>
    /// Storage of chats records
    /// </summary>
    public interface IChatRoomStorage : INamedStorage<ChatRoom>
    {
        /// <summary>
        /// Save new chat record
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> CreateChatAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update chat record
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateChatAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get chat record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChatRoomSelector> GetRecordAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get chats records by user key
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<KeyNameSelector>> GetRecordsByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get messages records of chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ChatMessageSelector>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Save message record
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CreateMessageAsync(ChatMessage model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check rights for operation
        /// </summary>
        /// <param name="role"></param>
        /// <param name="triggeredById"></param>
        /// <param name="boardId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsAllowToPerformOperationAsync(ChatRoomRoleType role, Guid triggeredById, Guid boardId, CancellationToken cancellationToken = default);
    }
}
