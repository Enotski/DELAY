using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class TicketsListEntity : KeyNamedModelEntity
    {
        public ICollection<TicketEntity> Tickets { get; set; }
    }
}
