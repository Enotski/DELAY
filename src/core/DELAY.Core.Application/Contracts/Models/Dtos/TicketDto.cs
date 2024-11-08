using DELAY.Core.Application.Contracts.Models.Dtos.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    /// <summary>
    /// Ticket model
    /// </summary>
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

    /// <summary>
    /// Ticket request model
    /// </summary>
    public class TicketRequestDto : KeyDto
    {
        public TicketRequestDto()
        {
        }

        public Guid BoardId { get; set; }
    }

    /// <summary>
    /// Ticket by list
    /// </summary>
    public class TicketsByListRequestDto
    {
        public TicketsByListRequestDto()
        {
        }

        public Guid ListId { get; set; }
        public Guid BoardId { get; set; }
    }

    public class UpdateTicketResultDto
    {
        public UpdateTicketResultDto(Guid? id, IEnumerable<Guid> ticketUsers, IEnumerable<Guid> removedTicketUsers, IEnumerable<Guid> newTicketUsers, bool success)
        {
            Id = id;
            TicketUsers = ticketUsers;
            RemovedTicketUsers = removedTicketUsers;
            NewTicketUsers = newTicketUsers;
            Success = success;
        }
        public Guid? Id { get; set; }
        public IEnumerable<Guid> TicketUsers { get; set; }
        public IEnumerable<Guid> RemovedTicketUsers { get; set; }
        public IEnumerable<Guid> NewTicketUsers { get; set; }
        public bool Success { get; set; }
    }
}
