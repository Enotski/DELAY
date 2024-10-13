using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors
{
    public class TicketSelector : KeyNameDto
    {
        public TicketSelector()
        {
        }

        public TicketSelector(Guid id, string name, bool isDone, string description, DateTime changedDate, DateTime createDate, string changedBy, string createdBy, DateTime deadLineDate, IEnumerable<KeyNameSelector> users) : base(id, name)
        {
            IsDone = isDone;
            Description = description;
            ChangedDate = changedDate;
            CreateDate = createDate;
            ChangedBy = changedBy;
            CreatedBy = createdBy;
            DeadLineDate = deadLineDate;
            Users = users;
        }

        public bool IsDone { get; set; }
        public string Description { get; set; }

        public DateTime ChangedDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }

        public DateTime DeadLineDate { get; set; }
        public IEnumerable<KeyNameSelector> Users { get; set; }
    }

}
