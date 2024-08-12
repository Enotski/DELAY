using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class TicketsListEntity : KeyNamedModelEntity
    {
        public ICollection<TicketEntity> Tickets { get; set; }
    }
}
