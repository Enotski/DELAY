using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class ChatRoomUserEntity : KeyEntity, IKey
    {
        public Guid ChatRoomId { get; set; }
        public ChatRoomEntity ChatRoom { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public ChatRoomRoleType UserRole { get; set; }
    }
}
