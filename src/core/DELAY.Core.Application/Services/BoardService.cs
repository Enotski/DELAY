using DELAY.Core.Application.Abstractions.Services.Boards;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Services
{
    internal class BoardService : BaseService, IBoardService
    {
        private readonly IBoardStorage boardStorage;

        private readonly IUserStorage userStorage;

        public BoardService(IBoardStorage boardStorage, IUserStorage userStorage)
        {
            this.boardStorage = boardStorage ?? throw new ArgumentNullException(nameof(IBoardStorage));

            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(IUserStorage));
        }

        private async Task<KeyNamedModel> ValidatePermissionToOperation(RoleType roleType, string triggeredByName, Guid? boardId = null)
        {
            var result = await userStorage.PermissionToPerformOperationAsync(roleType, triggeredByName);

            if (result == null)
            {
                throw new Exception("No permission for operation");
            }

            return result;
        }

        public async Task<Guid?> AddAsync(Board model, string triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Board));

            var triggered = await ValidatePermissionToOperation(Domain.Enums.RoleType.User, triggeredBy);

            var board = new Board(model.Name, triggered.Name);

            board.BoardUsers = new List<BoardUser>() { new BoardUser(board, new KeyNamedModel(triggered.Id, triggered.Name), Domain.Enums.BoardRoleType.Admin) };

            if (!board.IsValid())
                throw new ArgumentException("Invalid board data");

            return await boardStorage.AddAsync(board);
        }


        public Task<int> UpdateAsync(Board model, string triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id, string triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(IEnumerable<Guid> ids, string triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<Board> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Board>> GetBoardsAssignedToUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Board>> GetRecordsAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
