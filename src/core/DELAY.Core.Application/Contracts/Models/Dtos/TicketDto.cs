using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketDto : KeyNameDto
    {
        public TicketDto()
        {
        }

        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }

        public DateTime DeadLineDate { get; set; }

        public Guid TicketsListId { get; set; }

        public IEnumerable<KeyNameDto> AssignedUsers { get; set; }
    }

    public class EditTicketRequestDto : KeyNameDto
    {
        public EditTicketRequestDto()
        {
        }

        public bool IsDone { get; set; }
        public Guid BoardId { get; set; }
        public Guid TicketsListId { get; set; }
        public string Description { get; set; }

        public DateTime DeadLineDate { get; set; }

        public IEnumerable<KeyNameDto> Users { get; set; }
    }

    public class TicketRequestDto : KeyDto
    {
        public TicketRequestDto()
        {
        }

        public Guid BoardId { get; set; }
    }

    public class TicketsByListRequestDto
    {
        public TicketsByListRequestDto()
        {
        }

        public Guid ListId { get; set; }
        public Guid BoardId { get; set; }
    }
}
