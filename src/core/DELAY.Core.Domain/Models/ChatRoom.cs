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

        public ChatRoom(string name, RoomType chatType, List<ChatRoomUser> roomUsers, IEnumerable<KeyNamedModel> boards) : base(name)
        {
            RoomUsers = roomUsers;
            ChatType = chatType;
            Boards = boards;
        }

        public ChatRoom(Guid id, string name, RoomType chatType, List<ChatRoomUser> roomUsers, IEnumerable<KeyNamedModel> boards) : base(id, name)
        {
            RoomUsers = roomUsers;
            ChatType = chatType;
            Boards = boards;
        }

        public RoomType ChatType { get; set; }
        public IEnumerable<KeyNamedModel> Boards { get; set; }
        public List<ChatRoomUser> RoomUsers { get; set; } = new();
        public IEnumerable<ChatMessage> Messages { get; set; }

        public void SetUser(Guid userId, string name, ChatRoomRoleType role)
        {
            if (RoomUsers.Any(x => x.User.Id == userId))
            {
                RoomUsers = RoomUsers.Where(x => x.User.Id != userId).ToList();
            }

            RoomUsers.Add(new ChatRoomUser(null, new KeyNamedModel(userId, name), role));
        }

        public bool IsValid()
        {
            return base.IsValidName() && (RoomUsers?.Any() ?? false);
        }

        public void Update(string name, RoomType chatType, List<ChatRoomUser> roomUsers, IEnumerable<KeyNamedModel> boards)
        {
            RoomUsers = roomUsers;
            ChatType = chatType;
            Boards = boards;

            base.Update(name);
        }

    }
}
