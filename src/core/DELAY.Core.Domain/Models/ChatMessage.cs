namespace DELAY.Core.Domain.Models
{
    /// <summary>
    /// Message in chat room
    /// </summary>
    public class ChatMessage
    {
        public ChatMessage()
        {
        }

        public ChatMessage(Guid chatId, DateTime time, string author, string text)
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

        public bool IsValid() => ChatId != Guid.Empty && Time != DateTime.MaxValue || Time != DateTime.MinValue && !string.IsNullOrWhiteSpace(Author);
    }
}
