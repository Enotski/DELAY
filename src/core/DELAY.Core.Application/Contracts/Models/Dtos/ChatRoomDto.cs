using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    /// <summary>
    /// Chat model
    /// </summary>
    public class ChatRoomDto
    {
        public ChatRoomDto()
        {
        }

        public ChatRoomDto(Guid? id, string name, bool isPublic, RoomType chatType, IEnumerable<KeyNameDto> boards, IEnumerable<RoomUserDto> users)
        {
            Id = id;
            Name = name;
            IsPublic = isPublic;
            ChatType = chatType;
            Boards = boards;
            Users = users;
        }

        public Guid? Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public RoomType ChatType { get; set; }

        public IEnumerable<KeyNameDto> Boards { get; set; }

        public IEnumerable<RoomUserDto> Users { get; set; }
    }

    /// <summary>
    /// Chat-user model
    /// </summary>
    public class RoomUserDto
    {
        public RoomUserDto()
        {
        }

        public RoomUserDto(KeyNameDto user, ChatRoomRoleType role)
        {
            User = user;
            Role = role;
        }

        public KeyNameDto User { get; set; }

        public ChatRoomRoleType Role { get; set; }
    }

    /// <summary>
    /// Message model
    /// </summary>
    public class ChatMessageDto
    {
        public ChatMessageDto()
        {
        }

        public ChatMessageDto(Guid chatId, string time, string author, string text, bool isCurrentUserMessage)
        {
            ChatId = chatId;
            Time = time;
            Author = author;
            Text = text;
            IsCurrentUserMessage = isCurrentUserMessage;
        }

        public Guid ChatId { get; set; }

        public string Time { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public bool IsCurrentUserMessage { get; set; }
    }
}
