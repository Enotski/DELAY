using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class TicketsListDto
    {
        public TicketsListDto()
        {
        }

        public TicketsListDto(IEnumerable<KeyNameDto> tickets, Guid boardId)
        {
            Tickets = tickets;
            BoardId = boardId;
        }
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<KeyNameDto>? Tickets { get; set; }
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
            Id = ticketsListId;
        }

        public Guid BoardId { get; set; }
        public Guid Id { get; set; }
    }
}
