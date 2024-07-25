using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    /// <summary>
    /// Task in tracker
    /// </summary>
    public class TicketDto : KeyNameDto
    {
        public TicketDto()
        {
        }

        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }

        public KeyNameDto ChangedBy { get; set; }

        public ICollection<KeyNameDto> AssignedUsers { get; set; } = new List<KeyNameDto>();
    }
}
