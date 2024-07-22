using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class Ticket : KeyNamedModel
    {

        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }

        public User ChangedBy { get; set; }

        public ICollection<TicketUser> AssignedUsers { get; set; } = new List<TicketUser>();

        public TicketsList List { get; set; }
    }
}
