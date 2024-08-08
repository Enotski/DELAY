using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Board : KeyNamedModel, IDescriptioin
    {
        public Board(string name, string description, string createdBy) : base(name)
        {
            Description = description;
            CreateDate = DateTime.UtcNow;
            ChangeDate = CreateDate;
            CreatedBy = createdBy;
        }

        public Board(Guid id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public void Update(string name, string description, string changedBy)
        {
            Description = description;
            ChangeDate = DateTime.UtcNow;
            ChangedBy = changedBy;

            base.Update(name);
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(CreatedBy);
        }

        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    }
}
