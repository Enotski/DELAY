using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class Ticket : KeyNamedModel, IDescriptioin
    {
        public Ticket()
        {
        }

        public Ticket(KeyNamedModel ticketsList, string name, string description, string changedBy, IEnumerable<KeyNamedModel> assignedUsers) : base(name)
        {
            TicketsList = ticketsList;
            Description = description;
            ChangedDate = DateTime.UtcNow;
            Users = assignedUsers;
            ChangedBy = changedBy;
        }

        public bool IsDone { get; set; }
        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }

        public DateTime DeadLineDate { get; set; }

        public KeyNamedModel TicketsList { get; set; }
        public IEnumerable<KeyNamedModel> Users { get; set; } = new List<KeyNamedModel>();

        public bool IsValid()
            => base.IsValidName() && !string.IsNullOrEmpty(CreatedBy) && CreateDate != DateTime.MaxValue && CreateDate != DateTime.MaxValue && TicketsList != null && TicketsList.Id != Guid.Empty;


        public void Update(string name, string description, bool isDone, IEnumerable<KeyNamedModel> assignedUsers, string changedBy)
        {
            IsDone = isDone;
            Description = description;
            ChangedDate = DateTime.UtcNow;
            ChangedBy = changedBy;
            Users = assignedUsers;
            this.Update(name);
        }
    }
}
