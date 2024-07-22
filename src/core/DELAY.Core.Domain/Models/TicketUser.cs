namespace DELAY.Core.Domain.Models
{
    public class TicketUser
    {
        public Guid Id { get; set; }
        public Ticket Ticket { get; set; }
        public User User { get; set; }
    }
}
