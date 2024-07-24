using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Abstractions.Services
{
    internal interface ITicketService : INamedService<Ticket>
    {
        /// <summary>
        /// Create ticket
        /// </summary>
        /// <param name="ticket">Ticket model</param>
        /// <returns></returns>
        Task CreateAsync(Ticket user);

        /// <summary>
        /// Update ticket
        /// </summary>
        /// <param name="ticket">Ticket updated model</param>
        /// <returns></returns>
        Task UpdateAsync(Ticket ticket);
    }
}
