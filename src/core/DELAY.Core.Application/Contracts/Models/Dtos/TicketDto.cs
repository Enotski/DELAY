using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketDto : KeyNameDto
    {
        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }

        public DateTime DeadLineDate { get; set; }

        public IEnumerable<KeyNameDto> AssignedUsers { get; set; }
    }

    public class EditTicketRequestDto : KeyNameDto
    {
        public string Description { get; set; }

        public DateTime DeadLineDate { get; set; }

        public IEnumerable<KeyNameDto> AssignedUsers { get; set; }
    }
}
