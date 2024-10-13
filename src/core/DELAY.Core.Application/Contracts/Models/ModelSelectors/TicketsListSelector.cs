using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors
{
    public class TicketsListSelector : KeyNameDto
    {
        public TicketsListSelector(Guid id, string name, IEnumerable<KeyNameSelector> tickets) : base(id, name)
        {
            Tickets = tickets;
        }

        public IEnumerable<KeyNameSelector> Tickets { get; set; }
    }
}
