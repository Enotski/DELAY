using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class Ticket : KeyNamedModel, IDescriptioin
    {
        public Ticket(string name, string description, string changedBy, IEnumerable<User> assignedUsers) : base(name)
        {
            Description = description;
            ChangedDate = DateTime.UtcNow;
            AssignedUsers = assignedUsers;
            ChangedBy = changedBy;
        }

        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }

        public string ChangedBy { get; set; }

        public IEnumerable<User> AssignedUsers { get; set; } = new List<User>();

        public void Update(string name, string description, IEnumerable<User> assignedUsers, string changedBy)
        {
            Description = description;
            ChangedDate = DateTime.UtcNow;
            ChangedBy = changedBy;
            AssignedUsers = assignedUsers;
            this.Update(name);
        }
    }
}
