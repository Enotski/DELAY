using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardChatRoomEntity
    {
        public BoardChatRoomEntity()
        {
        }

        [Key]
        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }

        [Key]
        public Guid ChatRoomId { get; set; }
        public ChatRoomEntity ChatRoom { get; set; }
    }
}
