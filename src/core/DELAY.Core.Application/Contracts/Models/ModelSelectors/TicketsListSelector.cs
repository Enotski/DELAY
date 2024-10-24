using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors
{
    public class TicketsListSelector : KeyNameDto
    {
        public TicketsListSelector(Guid id, string name, Guid boardId, IEnumerable<KeyNameSelector> tickets) : base(id, name)
        {
            Tickets = tickets;
            BoardId = boardId;
        }
        public Guid BoardId { get; set; }
        public IEnumerable<KeyNameSelector> Tickets { get; set; }
    }
}
