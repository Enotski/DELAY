using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Users;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;
using System.Reflection;

namespace DELAY.Core.Application.Services
{
    internal class UserService : BaseService, IUserService
    {
        private readonly IPasswordHelper _passwordHelper;

        protected readonly IUserStorage userStorage;

        public UserService(IUserStorage userStorage, IPasswordHelper passwordHelper)
        {
            this._passwordHelper = passwordHelper ?? throw new ArgumentNullException(nameof(IPasswordHelper));

            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(IUserStorage));
        }

        private async Task<KeyNamedModel> ValidatePermissionToOperation(RoleType roleType, string triggeredByName)
        {
            var result = await userStorage.PermissionToPerformOperationAsync(roleType, triggeredByName);

            if (result == null)
            {
                throw new Exception("No permission for operation");
            }

            return result;
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

        public async Task<Guid?> AddAsync(User model, string triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            if (triggeredBy == null)
                throw new ArgumentNullException(nameof(triggeredBy));

            var triggered = await ValidatePermissionToOperation(Domain.Enums.RoleType.Admin, triggeredBy);

            model.Password = _passwordHelper.GetHash(model.Password);

            await ValidateUserAsync(model);

            var user = new User(model.Name, model.Email, model.PhoneNumber, model.Password, triggered.Name);

            return await userStorage.AddAsync(user);
        }

        public async Task<int> UpdateAsync(User model, string triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            var triggred = await ValidatePermissionToOperation(RoleType.User, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            var record = await userStorage.GetAsync(model.Id);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.Update(model.Name, model.Email, model.PhoneNumber, record.Role, triggred.Name);

            await ValidateUserAsync(record);

            return await userStorage.UpdateAsync(record);
        }

        public async Task<int> UpdatePasswordAsync(Guid id, string password, string triggeredBy)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));               

            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            var triggred = await ValidatePermissionToOperation(RoleType.User, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            var record = await userStorage.GetAsync(id);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.SetPassword(_passwordHelper.GetHash(password), triggred.Name);

            await ValidateUserAsync(record);

            return await userStorage.UpdateAsync(record);
        }

        public async Task<int> DeleteAsync(Guid id, string triggeredBy)
        {
            var triggred = await ValidatePermissionToOperation(RoleType.Admin, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            return await userStorage.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(IEnumerable<Guid> ids, string triggeredBy)
        {
            var triggred = await ValidatePermissionToOperation(RoleType.Admin, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            return await userStorage.DeleteAsync(ids);
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await userStorage.GetAsync(id);
        }

        public async Task<PagedData<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination)
        {
            return await userStorage.GetRecordsAsync(searchOptions, sortOptions, pagination);
        }

        public async Task<IReadOnlyList<KeyNamedModel>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids)
        {
            return await userStorage.GetKeyNameRecordsAsync(ids);
        }

        public async Task<IReadOnlyList<User>> GetRecordsAsync(IEnumerable<Guid> ids)
        {
            return await userStorage.GetAsync(ids);
        }

        public async Task<IEnumerable<User>> GetUsersByTicketAsync(Guid ticketId)
        {
            return await userStorage.GetAssigedUsersToTicketAsync(ticketId);
        }

        public async Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId)
        {
            return await userStorage.GetBoardUsersAsync(boardId);
        }

        public Task<Guid?> AddAsync(EditCreateUserRequestDto model, OperationUserInfo triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(EditCreateUserRequestDto model, OperationUserInfo triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePasswordAsync(UserPasswordRequestDto model, OperationUserInfo triggeredBy)
        {
            throw new NotImplementedException();
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

        Task<PagedData<UserDto>> IUserService.GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> CreateAsync(NameDto model, OperationUserInfo triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(IEnumerable<Guid> ids, OperationUserInfo triggeredBy)
        {
            throw new NotImplementedException();
        }

        Task<UserDto> INamedService<UserDto>.GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<UserDto>> INamedService<UserDto>.GetRecordsAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
