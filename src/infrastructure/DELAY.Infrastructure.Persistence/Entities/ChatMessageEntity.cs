using System.ComponentModel.DataAnnotations;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class ChatMessageEntity
    {
        [Key]
        public Guid ChatId { get; set; }

        public ChatRoomEntity Chat { get; set; }

        [Key]
        public DateTime Time { get; set; }

        [Key]
        public string Author { get; set; }

        public string Text { get; set; }
    }
}
