using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketsListDto : KeyNameDto
    {
        public TicketsListDto()
        {
        }

        public TicketsListDto(IEnumerable<KeyNameDto> tickets, Guid boardId)
        {
            Tickets = tickets;
            BoardId = boardId;
        }

        public IEnumerable<KeyNameDto> Tickets { get; set; }
        public Guid BoardId { get; set; }
    }

    public class TicketsListRequestDto
    {
        public TicketsListRequestDto()
        {
        }

        public TicketsListRequestDto(Guid boardId, Guid ticketsListId)
        {
            BoardId = boardId;
            TicketsListId = ticketsListId;
        }

        public Guid BoardId { get; set; }
        public Guid TicketsListId { get; set; }
    }
}
