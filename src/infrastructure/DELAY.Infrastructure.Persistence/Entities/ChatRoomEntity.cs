using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class ChatRoomEntity : KeyNamedEntity
    {
        public ICollection<ChatRoomUserEntity> ChatRoomUsers { get; set; } = new List<ChatRoomUserEntity>();
        public ICollection<BoardChatRoomEntity> BoardChatRooms { get; set; } = new List<BoardChatRoomEntity>();

        public ChatType ChatType { get; set; }
    }
}
