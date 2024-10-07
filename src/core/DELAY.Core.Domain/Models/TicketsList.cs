using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class TicketsList : KeyNamedModel
    {
        public TicketsList()
        {
        }

        public TicketsList(string name) : base(name)
        {
        }

        public TicketsList(Guid id, string name) : base(id, name)
        {
        }

        public Guid BoardId { get; set; }
        public Board Board { get; set; }

        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
