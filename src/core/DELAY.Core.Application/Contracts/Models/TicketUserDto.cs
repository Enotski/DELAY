namespace DELAY.Core.Application.Contracts.Models
{
    public class TicketUserDto
    {
        public Guid Id { get; set; }
        public TicketDto Ticket { get; set; }
        public UserDto User { get; set; }
    }
}
