using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardChatRoomEntity
    {
        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }

        public Guid ChatRoomId { get; set; }
        public ChatRoomEntity ChatRoom { get; set; }
    }
}
