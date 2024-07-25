using DELAY.Core.Application.Contracts.Models;

namespace DELAY.Core.Application.Abstractions.Services
{
    internal interface ITicketService : INamedService<TicketDto>
    {
        /// <summary>
        /// Create ticket
        /// </summary>
        /// <param name="ticket">Ticket model</param>
        /// <returns></returns>
        Task CreateAsync(TicketDto ticket);

        /// <summary>
        /// Update ticket
        /// </summary>
        /// <param name="ticket">Ticket updated model</param>
        /// <returns></returns>
        Task UpdateAsync(TicketDto ticket);

        /// <summary>
        /// Get tickets assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<TicketDto>> GetTicketsAssignedToUser(Guid userId);
    }
}
