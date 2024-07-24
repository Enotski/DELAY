using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class TicketUser : KeyModel
    {
        public Ticket Ticket { get; set; }
        public User User { get; set; }
    }
}
