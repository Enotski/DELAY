using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class TicketsListEntity : KeyNamedEntity
    {
        public TicketsListEntity()
        {
        }

        public ICollection<TicketEntity> Tickets { get; set; }

        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }
    }
}
