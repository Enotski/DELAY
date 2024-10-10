using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketListDto : KeyNameDto
    {
        public IEnumerable<KeyNameDto> Tickets { get; set; }
        public Guid BoardId { get; set; }
    }

    public class EditTicketsListRequestDto : KeyNameDto
    {
        public Guid BoardId { get; set; }
    }
}
