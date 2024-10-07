using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class ChatRoom : KeyNamedModel
    {
        public ChatRoom()
        {
        }

        public ChatRoom(string name, IEnumerable<ChatRoomUser> roomUsers) : base(name)
        {
            RoomUsers = roomUsers;
        }

        public ChatRoom(Guid id, string name, IEnumerable<ChatRoomUser> roomUsers) : base(id, name)
        {
            RoomUsers = roomUsers;
        }

        public ChatType ChatType { get; set; }

        public IEnumerable<Board> Boards { get; set; }
        public IEnumerable<ChatRoomUser> RoomUsers { get; set; }
    }
}
