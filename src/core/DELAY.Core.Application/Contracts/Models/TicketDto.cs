using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class TicketDto : KeyNameDto
    {

        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }

        public UserDto ChangedBy { get; set; }

        public ICollection<TicketUserDto> AssignedUsers { get; set; } = new List<TicketUserDto>();

        public TicketsListDto List { get; set; }
    }
}
