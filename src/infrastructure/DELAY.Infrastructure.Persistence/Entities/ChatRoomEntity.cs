﻿using DELAY.Core.Domain.Enums;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class ChatRoomEntity : KeyNamedEntity
    {
        public ChatRoomEntity()
        {
        }

        public ICollection<ChatRoomUserEntity> ChatRoomUsers { get; set; } = new List<ChatRoomUserEntity>();
        public ICollection<BoardChatRoomEntity> BoardChatRooms { get; set; } = new List<BoardChatRoomEntity>();
        public ICollection<ChatMessageEntity> Messages { get; set; } = new List<ChatMessageEntity>();

        public RoomType ChatType { get; set; }
    }
}
