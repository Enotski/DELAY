using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Abstractions.Services.Rooms
{
    /// <summary>
    /// Chats service
    /// </summary>
    public interface IChatRoomService : INamedService
    {
        /// <summary>
        /// Add chat model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<Guid?> CreateAsync(ChatRoomDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Edit chat model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(ChatRoomDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get chat model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<ChatRoomDto> GetAsync(Guid id, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get chats by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetByUserAsync(Guid userId);

        /// <summary>
        /// Remove chat
        /// </summary>
        /// <param name="id"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get messages records
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<IEnumerable<ChatMessageDto>> GetMessagesAsync(Guid chatId, OperationUserInfo triggeredBy);

        /// <summary>
        /// Create message record
        /// </summary>
        /// <param name="model"></param>
        /// <param name="triggeredBy"></param>
        /// <returns></returns>
        Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto model, OperationUserInfo triggeredBy);
    }
}
