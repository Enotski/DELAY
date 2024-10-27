using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketDto 
    {
        public TicketDto()
        {
        }
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public bool IsDone { get; set; }
        public string Description { get; set; }

        public DateTime? ChangedDate { get; set; }
        public DateTime? CreateDate { get; set; }

        public string? ChangedBy { get; set; }
        public string? CreatedBy { get; set; }

        public Guid BoardId { get; set; }
        public Guid TicketListId { get; set; }

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
