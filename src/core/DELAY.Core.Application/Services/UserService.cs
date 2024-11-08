using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Services.Users;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Services
{
    internal class UserService : BaseService, IUserService
    {
        private readonly IPasswordHelper _passwordHelper;

        protected readonly IUserStorage userStorage;

        private readonly IModelMapperService modelMapperService;

        public UserService(IUserStorage userStorage, IPasswordHelper passwordHelper, IModelMapperService modelMapperService)
        {
            this._passwordHelper = passwordHelper ?? throw new ArgumentNullException(nameof(IPasswordHelper));

            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(IUserStorage));

            this.modelMapperService = modelMapperService ?? throw new ArgumentNullException(nameof(IModelMapperService));
        }

        protected async Task<bool> IsGlobalAllowToPerformOperationAsync(RoleType roleType, Guid triggeredById)
        {
            if (!await userStorage.IsAllowToPerformOperationAsync(roleType, triggeredById))
                throw new Exception("No permission for operation");

            return true;
        }

        private async Task ValidateUserAsync(User user)
        {
            if (!user.IsValidCredentials(false))
                throw new ArgumentException("Invalid user data");

            if (!string.IsNullOrWhiteSpace(user.Name) && !await userStorage.IsUniqueName(user.Name, user.Id))
                throw new ArgumentException("Such name already exist");

            if (!string.IsNullOrWhiteSpace(user.Email) && !await userStorage.IsUniqueEmail(user.Email, user.Id))
                throw new ArgumentException("Such email already exist");

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && !await userStorage.IsUniquePhone(user.PhoneNumber, user.Id))
                throw new ArgumentException("Such phone already exist");
        }

        public async Task<Guid> AddAsync(UserDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(UserDto));

            await IsGlobalAllowToPerformOperationAsync(Domain.Enums.RoleType.User, triggeredBy.Id);

            model.Password = _passwordHelper.GetHash(model.Password);

            var user = new User(model.Name, model.Email, model.PhoneNumber, model.Password, triggeredBy.Name);

            await ValidateUserAsync(user);

            return await userStorage.AddAsync(user);
        }

        public async Task<int> UpdateAsync(UserDto model, OperationUserInfo triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            await IsGlobalAllowToPerformOperationAsync(RoleType.User, triggeredBy.Id);

            var entity = await userStorage.GetAsync(model.Id.Value);
            if (entity == null)
                throw new ArgumentException("Not found");

            entity.Update(model.Name, model.Email, model.PhoneNumber, model.Role, triggeredBy.Name);

            await ValidateUserAsync(entity);

            return await userStorage.UpdateAsync(entity);
        }

        public async Task<int> UpdatePasswordAsync(UserPasswordRequestDto model, OperationUserInfo triggeredBy)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(model.Password, nameof(model.Password));

            if (model.Id == Guid.Empty)
                throw new ArgumentException(nameof(model.Id));

            var triggred = await IsGlobalAllowToPerformOperationAsync(RoleType.User, triggeredBy.Id);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            var record = await userStorage.GetAsync(model.Id);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.SetPassword(_passwordHelper.GetHash(model.Password), triggeredBy.Name);

            await ValidateUserAsync(record);

            return await userStorage.UpdateAsync(record);
        }

        public async Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy)
        {
            var triggred = await IsGlobalAllowToPerformOperationAsync(RoleType.Admin, triggeredBy.Id);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            return await userStorage.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(IEnumerable<Guid> ids, OperationUserInfo triggeredBy)
        {
            var triggred = await IsGlobalAllowToPerformOperationAsync(RoleType.Admin, triggeredBy.Id);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            return await userStorage.DeleteAsync(ids);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var res = await userStorage.GetAsync(id);

            return modelMapperService.Map<UserDto>(res);
        }

        public async Task<PagedData<UserDto>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination)
        {
            var res = await userStorage.GetRecordsAsync(searchOptions, sortOptions, pagination);

            return new PagedData<UserDto>(res.TotalCount, modelMapperService.Map<IEnumerable<UserDto>>(res.Data));
        }


        public async Task<IEnumerable<KeyNameDto>> GetKeyNameRecordsAsync(OperationUserInfo triggeredBy)
        {
            var res = await userStorage.GetKeyNameRecordsAsync();

            return modelMapperService.Map<IEnumerable<KeyNameDto>>(res);
        }

        public Task<IEnumerable<UserDto>> GetByTicketAsync(Guid ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetByBoardAsync(Guid boardId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetByChatRoomAsync(Guid boardId)
        {
            throw new NotImplementedException();
        }
    }
}
