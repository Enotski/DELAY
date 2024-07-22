namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class TicketUserEntity
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public TicketEntity Ticket { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
