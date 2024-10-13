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
        private readonly ITicketsListStorage ticketsListStorage;
        private readonly ITicketStorage ticketStorage;
        private readonly IUserStorage userStorage;
        private readonly IModelMapperService mapperService;

        public BoardService(IBoardStorage boardStorage, IUserStorage userStorage)
        {
            this.boardStorage = boardStorage ?? throw new ArgumentNullException(nameof(IBoardStorage));
        }

        protected async Task<bool> IsGlobalAllowToPerformOperationAsync(RoleType roleType, Guid triggeredById)
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

        public async Task<Guid?> CreateBoardAsync(BoardDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Board));

            await IsGlobalAllowToPerformOperationAsync(Domain.Enums.RoleType.User, triggeredBy.Id);

            var board = new Board(model.Name, model.Description, model.IsPublic, triggeredBy.Name);

            board.SetUser(triggeredBy.Id, triggeredBy.Name, Domain.Enums.BoardRoleType.Admin);

            if (!board.IsValid())
                throw new ArgumentException("Invalid board data");

            return await boardStorage.CreateBoardAsync(board);
        }

        public async Task<int> UpdateBoardAsync(BoardDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(BoardDto));

            await IsAllowToPerformOperationAsync(BoardRoleType.Admin, triggeredBy.Id, model.Id);

            var entity = await boardStorage.GetAsync(model.Id);
            if (entity == null)
                throw new ArgumentException("Not found");

            var users = mapperService.Map<IList<BoardUser>>(model.Users);
            entity.Update(model.Name, model.Description, model.IsPublic, users, triggeredBy.Name);

            if (!entity.IsValid())
                throw new ArgumentException("Invalid board data");

            return await boardStorage.UpdateBoardAsync(entity);
        }

        public async Task<BoardDto> GetBoardAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.User, triggeredBy.Id, id);

            var entity = await boardStorage.GetRecordAsync(id);
            if (entity == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<BoardDto>(entity);
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetBoardByUserAsync(Guid userId)
        {
            await IsGlobalAllowToPerformOperationAsync(RoleType.User, userId);

            var entities = await boardStorage.GetRecordsByUserAsync(userId);

            return mapperService.Map<IReadOnlyList<KeyNameDto>>(entities);
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetBoardByChatAsync(Guid chatId, OperationUserInfo triggeredBy)
        {
            await IsGlobalAllowToPerformOperationAsync(RoleType.User, triggeredBy.Id);

            var entities = await boardStorage.GetKeyNameRecordsByChatAsync(chatId);

            return mapperService.Map<IReadOnlyList<KeyNameDto>>(entities);
        }

        public async Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.Admin, triggeredBy.Id, id);

            return await boardStorage.DeleteAsync(id);
        }

        public async Task<TicketsListDto> GetTicketsListAsync(TicketsListRequestDto model, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.User, triggeredBy.Id, model.BoardId);

            var entity = await ticketsListStorage.GetRecordAsync(model.TicketsListId);
            if (entity == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<TicketsListDto>(entity);
        }

        public async Task<IEnumerable<TicketsListDto>> GetTicketsListByBoardAsync(Guid boardId, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.User, triggeredBy.Id, boardId);

            var entities = await ticketsListStorage.GetRecordsByBoardAsync(boardId);
            if (entities == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<IEnumerable<TicketsListDto>>(entities);
        }

        public async Task<Guid?> CreateTicketsListAsync(TicketsListDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Board));

            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, model.BoardId);

            var list = new TicketsList(model.Id, model.BoardId, model.Name);

            if (!list.IsValid())
                throw new ArgumentException("Invalid list data");

            return await ticketsListStorage.AddAsync(list);
        }

        public async Task<int> UpdateTicketsListAsync(TicketsListDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Board));

            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, model.BoardId);

            var entity = await ticketsListStorage.GetAsync(model.Id);

            entity.Update(model.Name);

            if (!entity.IsValid())
                throw new ArgumentException("Invalid list data");

            return await ticketsListStorage.UpdateAsync(entity);
        }

        public async Task<int> DeleteTicketsListAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, id);

            return await ticketsListStorage.DeleteAsync(id);
        }

        public async Task<Guid?> CreateTicketAsync(EditTicketRequestDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(TicketDto));

            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, model.BoardId);

            if(!await ticketsListStorage.IsExistById(model.TicketsListId) || !await boardStorage.IsExistById(model.BoardId))
                throw new ArgumentException("Tickets list or board not exist");

            var ticket = new Ticket(new KeyNamedModel(model.TicketsListId, ""), model.Name, model.Description, triggeredBy.Name, mapperService.Map<IEnumerable<KeyNamedModel>>(model.Users));

            if (!ticket.IsValid())
                throw new ArgumentException("Invalid list data");

            return await ticketStorage.CreateTicketAsync(ticket);
        }

        public async Task<int> UpdateTicketAsync(EditTicketRequestDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(TicketDto));

            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, model.BoardId);

            if (!await ticketsListStorage.IsExistById(model.TicketsListId) || !await boardStorage.IsExistById(model.BoardId))
                throw new ArgumentException("Tickets list or board not exist");

            var entity = await ticketStorage.GetAsync(model.Id);
            if(entity == null)
                throw new ArgumentException("Ticket is not exist");

            entity.Update(model.Name, model.Description, model.IsDone, mapperService.Map<IEnumerable<KeyNamedModel>>(model.Users), triggeredBy.Name);

            if (!entity.IsValid())
                throw new ArgumentException("Invalid list data");

            return await ticketStorage.UpdateTicketAsync(entity);
        }

        public async Task<TicketDto> GetTicketAsync(TicketRequestDto model, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.User, triggeredBy.Id, model.BoardId);

            var entity = await ticketsListStorage.GetRecordAsync(model.Id);
            if (entity == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<TicketDto>(entity);
        }

        public async Task<IEnumerable<KeyNameDto>> GetTicketsByListAsync(TicketsByListRequestDto model, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.User, triggeredBy.Id, model.BoardId);

            var entities = await ticketStorage.GetRecordsByListAsync(model.ListId);
            if (entities == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<IEnumerable<KeyNameDto>>(entities);
        }

        public async Task<IEnumerable<TicketsListDto>> GetTicketsByUserAsync(OperationUserInfo triggeredBy)
        {
            await IsGlobalAllowToPerformOperationAsync(RoleType.User, triggeredBy.Id);

            var entities = await ticketStorage.GetRecordsByUserAsync(triggeredBy.Id);
            if (entities == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<IEnumerable<TicketsListDto>>(entities);
        }

        public async Task<int> DeleteTicketAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(BoardRoleType.Moderator, triggeredBy.Id, id);

            return await ticketStorage.DeleteAsync(id);
        }
    }
}
