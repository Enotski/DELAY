using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Board : KeyNamedModel
    {
        public Board(string name, string createdBy) : base(name)
        {
            CreateDate = DateTime.UtcNow;
            ChangeDate = CreateDate;
            CreatedBy = createdBy;
        }

        public Board(Guid id, string name) : base(id, name)
        {
        }

        public Board()
        {
        }

        public void Update(string name, string changedBy)
        {
            ChangeDate = DateTime.UtcNow;
            ChangedBy = changedBy;

            base.Update(name);
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(CreatedBy);
        }

        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreateDate { get; set; }

        public IEnumerable<TicketsList> TicketsLists { get; set; }
        public IEnumerable<ChatRoom> ChatRooms { get; set; }
        public IEnumerable<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    }
}
