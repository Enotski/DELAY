namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class TicketUserEntity: KeyModelEntity
    {
        public Guid TicketId { get; set; }
        public TicketEntity Ticket { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
