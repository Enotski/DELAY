using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Board : KeyNamedModel
    {
        public Board(string name, string description, bool isPublic, string createdBy) : base(name)
        {
            CreateDate = DateTime.UtcNow;
            ChangeDate = CreateDate;
            CreatedBy = createdBy;
            IsPublic = isPublic;
            Description = description;
        }

        public Board(Guid id, string name) : base(id, name)
        {
        }

        public Board()
        {
        }

        public void Update(string name, string description, bool isPublic, IList<BoardUser> boardUsers, string changedBy)
        {
            IsPublic = isPublic;
            Description = description;
            ChangeDate = DateTime.UtcNow;
            ChangedBy = changedBy;
            BoardUsers = boardUsers;

            base.Update(name);
        }

        public void SetUser(Guid userId, string name, BoardRoleType role)
        {
            BoardUsers.Add(new BoardUser(new KeyNamedModel(userId, name), role));
        }

        public bool IsValid()
        {
            return base.IsValidName() && !string.IsNullOrWhiteSpace(CreatedBy) && (!IsPublic && (BoardUsers?.Any() ?? false));
        }

        public bool IsPublic { get; set; }
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreateDate { get; set; }

        public IEnumerable<TicketsList> TicketsLists { get; set; }
        public IEnumerable<ChatRoom> ChatRooms { get; set; }
        public IList<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    }
}
