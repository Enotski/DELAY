using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Rooms
{
    public interface IChatRoomService : INamedService
    {
        Task<Guid?> CreateAsync(ChatRoomDto model, OperationUserInfo triggeredBy);
        Task<int> UpdateAsync(ChatRoomDto model, OperationUserInfo triggeredBy);
        Task<ChatRoomDto> GetAsync(Guid id, OperationUserInfo triggeredBy);
        Task<IReadOnlyList<KeyNameDto>> GetByUserAsync(Guid userId);
        Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy);
        Task<IEnumerable<ChatMessageDto>> GetMessagesAsync(Guid chatId, OperationUserInfo triggeredBy);
        Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto model, OperationUserInfo triggeredBy);
    }
}
