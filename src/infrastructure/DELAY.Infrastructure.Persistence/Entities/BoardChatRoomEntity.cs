using System.ComponentModel.DataAnnotations;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardChatRoomEntity
    {
        public BoardChatRoomEntity()
        {
        }

        public BoardChatRoomEntity(Guid boardId, Guid chatRoomId)
        {
            BoardId = boardId;
            ChatRoomId = chatRoomId;
        }

        [Key]
        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }

        [Key]
        public Guid ChatRoomId { get; set; }
        public ChatRoomEntity ChatRoom { get; set; }
    }
}
