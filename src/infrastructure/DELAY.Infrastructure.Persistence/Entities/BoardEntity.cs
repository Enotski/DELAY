﻿using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardEntity : KeyNamedEntity
    {
        public BoardEntity()
        {
        }

        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
        public string Description { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPublic { get; set; }

        public ICollection<BoardChatRoomEntity> BoardChatRooms { get; set; } = new List<BoardChatRoomEntity>();
        public ICollection<BoardUserEntity> BoardUsers { get; set; } = new List<BoardUserEntity>();
        public ICollection<TicketsListEntity> TicketsLists { get; set; } = new List<TicketsListEntity>();
    }
}
