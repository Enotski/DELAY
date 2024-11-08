using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Services.Rooms;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Services
{
    internal class ChatRoomService : BaseService, IChatRoomService
    {
        private readonly IChatRoomStorage chatStorage;
        private readonly IUserStorage userStorage;
        private readonly IModelMapperService mapperService;

        public ChatRoomService(IChatRoomStorage chatStorage, IUserStorage userStorage, IModelMapperService mapperService)
        {
            this.chatStorage = chatStorage ?? throw new ArgumentNullException(nameof(IChatRoomStorage));
            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(IUserStorage));
            this.mapperService = mapperService ?? throw new ArgumentNullException(nameof(IModelMapperService));
        }

        protected async Task<bool> IsGlobalAllowToPerformOperationAsync(RoleType roleType, Guid triggeredById)
        {
            if (!await userStorage.IsAllowToPerformOperationAsync(roleType, triggeredById))
                throw new Exception("No permission for operation");

            return true;
        }

        private async Task<bool> IsAllowToPerformOperationAsync(ChatRoomRoleType roleType, Guid triggeredById, Guid chatId)
        {
            if (!await chatStorage.IsAllowToPerformOperationAsync(roleType, triggeredById, chatId))
                throw new Exception("No permission for operation");

            return true;
        }

        public async Task<Guid?> CreateAsync(ChatRoomDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatRoomDto));

            await IsGlobalAllowToPerformOperationAsync(Domain.Enums.RoleType.User, triggeredBy.Id);

            var entity = new ChatRoom(model.Name, model.ChatType, mapperService.Map<List<ChatRoomUser>>(model.Users), mapperService.Map<IEnumerable<KeyNamedModel>>(model.Boards));

            entity.SetUser(triggeredBy.Id, triggeredBy.Name, Domain.Enums.ChatRoomRoleType.Admin);

            if (!entity.IsValid())
                throw new ArgumentException("Invalid board data");

            return await chatStorage.CreateChatAsync(entity);
        }

        public async Task<int> UpdateAsync(ChatRoomDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatRoomDto));

            await IsAllowToPerformOperationAsync(ChatRoomRoleType.Admin, triggeredBy.Id, model.Id.Value);

            var entity = await chatStorage.GetAsync(model.Id.Value);
            if (entity == null)
                throw new ArgumentException("Not found");

            entity.Update(model.Name, model.ChatType, mapperService.Map<List<ChatRoomUser>>(model.Users), mapperService.Map<IEnumerable<KeyNamedModel>>(model.Boards));

            if (!entity.IsValid())
                throw new ArgumentException("Invalid board data");

            return await chatStorage.UpdateChatAsync(entity);
        }

        public async Task<ChatRoomDto> GetAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(ChatRoomRoleType.User, triggeredBy.Id, id);

            var entity = await chatStorage.GetRecordAsync(id);
            if (entity == null)
                throw new ArgumentException("Not found");

            return mapperService.Map<ChatRoomDto>(entity);
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetByUserAsync(Guid userId)
        {
            await IsGlobalAllowToPerformOperationAsync(RoleType.User, userId);

            var entities = await chatStorage.GetRecordsByUserAsync(userId);

            return mapperService.Map<IReadOnlyList<KeyNameDto>>(entities);
        }

        public async Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(ChatRoomRoleType.Admin, triggeredBy.Id, id);

            return await chatStorage.DeleteAsync(id);
        }

        public async Task<IEnumerable<ChatMessageDto>> GetMessagesAsync(Guid chatId, OperationUserInfo triggeredBy)
        {
            await IsAllowToPerformOperationAsync(ChatRoomRoleType.User, triggeredBy.Id, chatId);

            var entities = await chatStorage.GetMessagesAsync(chatId);
            if (entities == null)
                throw new ArgumentException("Not found");

            var res = mapperService.Map<IEnumerable<ChatMessageDto>>(entities.OrderBy(x => x.Time)).ToList();

            return res;
        }

        public async Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(ChatMessageDto));

            await IsAllowToPerformOperationAsync(ChatRoomRoleType.User, triggeredBy.Id, model.ChatId);

            var message = new ChatMessage(model.ChatId, DateTime.Parse(model.Time).ToUniversalTime(), triggeredBy.Name, model.Text);

            if (!message.IsValid())
                throw new ArgumentException("Invalid message format");

            await chatStorage.CreateMessageAsync(message);

            return mapperService.Map<ChatMessageDto>(message);
        }
    }
}
