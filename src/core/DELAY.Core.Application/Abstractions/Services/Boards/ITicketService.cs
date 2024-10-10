using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Abstractions.Services.Boards
{
    internal interface ITicketService : INamedService<TicketDto>
    {
        Task<Guid> CreateAsync(EditTicketRequestDto model, OperationUserInfo triggeredBy);

        Task<int> UpdateAsync(EditTicketRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get tickets assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetByUserAsync(Guid userId);
    }
}
