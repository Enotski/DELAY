using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketsListDto : KeyNameDto
    {
        public IEnumerable<KeyNameDto> Tickets { get; set; }
        public Guid BoardId { get; set; }
    }

    public class TicketsListRequestDto
    {
        public Guid BoardId { get; set; }
        public Guid TicketsListId { get; set; }
    }
}
