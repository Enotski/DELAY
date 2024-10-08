using DELAY.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class ChatRoomUserEntity
    {
        [Key]
        public Guid ChatRoomId { get; set; }
        public ChatRoomEntity ChatRoom { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public ChatRoomRoleType UserRole { get; set; }
    }
}
