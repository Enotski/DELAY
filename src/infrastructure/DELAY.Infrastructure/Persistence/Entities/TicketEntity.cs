using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class TicketEntity : KeyNamedModelEntity, IDescriptioin
    {

        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }

        public Guid ChangedById { get; set; }
        public UserEntity ChangedBy { get; set; }

        public Guid ListId { get; set; }
        public TicketsListEntity List { get; set; }

        public ICollection<TicketUserEntity> AssignedUsers { get; set; } = new List<TicketUserEntity>();
    }
}
