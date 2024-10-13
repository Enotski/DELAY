using System.ComponentModel.DataAnnotations;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class TicketUserEntity 
    {
        public TicketUserEntity()
        {
        }

        public TicketUserEntity(Guid ticketId, Guid userId)
        {
            TicketId = ticketId;
            UserId = userId;
        }

        [Key]
        public Guid TicketId { get; set; }
        public TicketEntity Ticket { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
