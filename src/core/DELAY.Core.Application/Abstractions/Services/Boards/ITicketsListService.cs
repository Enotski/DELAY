using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Abstractions.Services.Boards
{
    public interface ITicketsListService : INamedService<TicketListDto>
    {
        Task<Guid> CreateAsync(EditTicketsListRequestDto model, OperationUserInfo triggeredBy);

        Task<int> UpdateAsync(EditTicketsListRequestDto model, OperationUserInfo triggeredBy);

        /// <summary>
        /// Get boards assigned to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<IReadOnlyList<KeyNameDto>> GetByBoardAsync(Guid boardId);
    }
}
