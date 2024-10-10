using DELAY.Core.Application.Abstractions.Services.Boards;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Services
{
    internal class BoardService : BaseService, IBoardService
    {
        private readonly IBoardStorage boardStorage;

        private readonly IUserStorage userStorage;

        private readonly IModelMapperService modelMapperService;

        public BoardService(IBoardStorage boardStorage, IUserStorage userStorage)
        {
            this.boardStorage = boardStorage ?? throw new ArgumentNullException(nameof(IBoardStorage));

            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(IUserStorage));
        }

        // TODO - move to userService/baseService
        private async Task<bool> IsGlobalAllowToPerformOperationAsync(RoleType roleType, Guid triggeredById)
        {
            if (!await userStorage.IsAllowToPerformOperationAsync(roleType, triggeredById))
                throw new Exception("No permission for operation");

            return true;
        }

        private async Task<bool> IsAllowToPerformOperationAsync(BoardRoleType roleType, Guid triggeredById, Guid boardId)
        {
            if (!await boardStorage.IsAllowToPerformOperationAsync(roleType, triggeredById, boardId))
                throw new Exception("No permission for operation");

            return true;
        }

        public async Task<Guid?> CreateAsync(NameDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Board));

            await IsGlobalAllowToPerformOperationAsync(Domain.Enums.RoleType.User, triggeredBy.Id);

            var board = new Board(model.Name, triggeredBy.Name);

            board.BoardUsers = new List<BoardUser>()
            {
                new BoardUser(board, new KeyNamedModel(triggeredBy.Id, triggeredBy.Name), Domain.Enums.BoardRoleType.Admin)
            };

            if (!board.IsValid())
                throw new ArgumentException("Invalid board data");

            return await boardStorage.CreateBoardAsync(board);
        }

        public async Task<int> UpdateAsync(EditBoardRequestDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(EditBoardRequestDto));

            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, model.Id);

            var entity = await boardStorage.GetAsync(model.Id);
            if(entity == null)
                throw new ArgumentException("Not found");

            entity.Update(model.Name, model.Description, triggeredBy.Name);

            if (!entity.IsValid())
                throw new ArgumentException("Invalid board data");

            return await boardStorage.UpdateAsync(entity);
        }

        public async Task<BoardDto> GetAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.User, triggeredBy.Id, id);

            var entity = await boardStorage.GetBoardAsync(id);
            if (entity == null)
                throw new ArgumentException("Not found");

            return modelMapperService.Map<BoardDto>(entity);
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetByUserAsync(Guid userId)
        {
            await IsGlobalAllowToPerformOperationAsync(RoleType.User, userId);

            var entities = await boardStorage.GetByUserAsync(userId);

            return modelMapperService.Map<IReadOnlyList<KeyNameDto>>(entities);
        }

        public async Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.Admin, triggeredBy.Id, id);

            return await boardStorage.DeleteAsync(id);
        }
    }
}
