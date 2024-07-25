using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class TicketUserDto : KeyDto
    {
        public TicketDto Ticket { get; set; }
        public UserDto User { get; set; }
    }
}
