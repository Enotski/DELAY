using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class TicketUserEntity : KeyEntity
    {
        public Guid TicketId { get; set; }
        public TicketEntity Ticket { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
