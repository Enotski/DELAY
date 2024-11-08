using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors
{
    public class ChatRoomSelector : KeyNameDto
    {
        public ChatRoomSelector(Guid id, string name, RoomType chatType, IEnumerable<KeyNameSelector> boards, IEnumerable<ChatRoomUserSelector> users) : base(id, name)
        {
            ChatType = chatType;
            Boards = boards;
            Users = users;
        }

        public RoomType ChatType { get; set; }

        public IEnumerable<KeyNameSelector> Boards { get; set; }
        public IEnumerable<ChatRoomUserSelector> Users { get; set; }
    }

    public class ChatRoomUserSelector
    {
        public ChatRoomUserSelector(KeyNameSelector user, ChatRoomRoleType userRole)
        {
            User = user;
            UserRole = userRole;
        }

        public KeyNameSelector User { get; set; }
        public ChatRoomRoleType UserRole { get; set; }
    }

    public class ChatMessageSelector
    {
        public ChatMessageSelector()
        {
        }

        public ChatMessageSelector(Guid chatId, DateTime time, string author, string text)
        {
            ChatId = chatId;
            Time = time;
            Author = author;
            Text = text;
        }

        public Guid ChatId { get; set; }
        public DateTime Time { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
    }
}
