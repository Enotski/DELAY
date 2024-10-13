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

        public TicketsList(Guid id, Guid boardId, string name) : base(id, name)
        {
            BoardId = boardId;
        }

        public bool IsValid()
            => IsValidName() && BoardId != Guid.Empty;

        public Guid BoardId { get; set; }

        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
