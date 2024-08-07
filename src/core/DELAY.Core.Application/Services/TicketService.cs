using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Services
{
    internal class TicketService : ITicketService
    {
        private readonly ITicketStorage ticketStorage;

        private readonly IModelMapperService mapper;

        public TicketService(ITicketStorage ticketStorage, IModelMapperService mapper)
        {
            this.ticketStorage = ticketStorage ?? throw new ArgumentNullException(nameof(ticketStorage));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAsync(TicketDto ticket)
        {
            try
            {
                var ticketChangedBy = new User(ticket.ChangedBy.Id, ticket.ChangedBy.Name);

                var assignedUsers = ticket.AssignedUsers.Select(user => new User(user.Id, user.Name));

                var newTicket = new Ticket(ticket.Name, ticket.Description, ticketChangedBy, assignedUsers);

                await ticketStorage.AddAsync(newTicket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateAsync(TicketDto ticket)
        {
            try
            {
                var record = await ticketStorage.GetAsync(ticket.Id);

                if (record == null)
                {
                    throw new Exception("Record not found");
                }

                var ticketChangedBy = new User(ticket.ChangedBy.Id, ticket.ChangedBy.Name);

                var assignedUsers = ticket.AssignedUsers.Select(user => new User(user.Id, user.Name));

                record.Update(ticket.Name, ticket.Description, assignedUsers, ticketChangedBy);

                await ticketStorage.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, string triggeredBy)
        {
            try
            {
                await ticketStorage.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids, string triggeredBy)
        {
            try
            {
                await ticketStorage.DeleteAsync(ids);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                return await ticketStorage.GetKeyNameRecordsAsync(ids);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<SearchOptions> searchOptions, SortOptions sortOption, PaginationOptions pagination)
        {
            try
            {
                return await ticketStorage.GetKeyNameRecordsAsync(searchOptions, sortOption, pagination);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TicketDto> GetAsync(Guid id)
        {
            try
            {
                var result = await ticketStorage.GetAsync(id);

                return mapper.Map<TicketDto>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<TicketDto>> GetRecordsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var result = await ticketStorage.GetAsync(ids);

                return mapper.Map<IReadOnlyList<TicketDto>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<TicketDto>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination)
        {
            try
            {
                var result = await ticketStorage.GetRecordsAsync(searchOptions, sortOptions, pagination);

                return mapper.Map<IReadOnlyList<TicketDto>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<TicketDto>> GetTicketsAssignedToUser(Guid userId)
        {
            try
            {
                return await ticketStorage.GetAssignedToUserAsync(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
