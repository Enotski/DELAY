using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services.Tickets
{
    internal interface ITicketService : INamedService<Ticket>
    {
        /// <summary>
        /// Create ticket
        /// </summary>
        /// <param name="ticket">Ticket model</param>
        /// <returns></returns>
        Task<Guid> CreateAsync(Ticket ticket);

        /// <summary>
        /// Update ticket
        /// </summary>
        /// <param name="ticket">Ticket updated model</param>
        /// <returns></returns>
        Task<int> UpdateAsync(Ticket ticket);

        /// <summary>
        /// Get tickets assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<Ticket>> GetTicketsAssignedToUser(Guid userId);
    }
}
