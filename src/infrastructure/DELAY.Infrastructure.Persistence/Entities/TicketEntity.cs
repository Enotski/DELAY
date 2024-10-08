using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class TicketEntity : KeyNamedEntity, IDescriptioin
    {
        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }

        public Guid TicketListId { get; set; }
        public TicketsListEntity TicketList { get; set; }

        public ICollection<TicketUserEntity> AssignedUsers { get; set; } = new List<TicketUserEntity>();
    }
}
