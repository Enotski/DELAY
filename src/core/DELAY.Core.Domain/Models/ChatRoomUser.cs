using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class ChatRoomUser
    {
        public ChatRoomUser()
        {
        }

        public ChatRoomUser(KeyNamedModel? room, KeyNamedModel user, ChatRoomRoleType userRole)
        {
            Room = room;
            User = user;
            UserRole = userRole;
        }

        public KeyNamedModel? Room { get; set; }
        public KeyNamedModel User { get; set; }
        public ChatRoomRoleType UserRole { get; set; }
    }
}
