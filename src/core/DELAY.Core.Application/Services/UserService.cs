using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Abstractions.Services.Base;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Services
{
    internal class UserService : BaseService, IUserService
    {
        private readonly ICryptographyService cryptoService;

        protected readonly IUserStorage userStorage;

        public UserService(IUserStorage userStorage, ICryptographyService cryptoService)
        {
            this.cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(ICryptographyService));

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
            if (!user.IsValidCredentials())
                throw new ArgumentException("Invalid user data");

            if (!await userStorage.IsUniqueName(user.Name))
                throw new ArgumentException("Such name already exist");

            if (!await userStorage.IsUniqueEmail(user.Email))
                throw new ArgumentException("Such email already exist");

            if (!await userStorage.IsUniquePhone(user.PhoneNumber))
                throw new ArgumentException("Such phone already exist");
        }

        public async Task<Guid?> AddAsync(User model, string triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            var triggered = await ValidatePermissionToOperation(Domain.Enums.RoleType.Administrator, triggeredBy);

            model.Password = cryptoService.GetHash(model.Password);

            await ValidateUserAsync(model);

            var user = new User(model.Name, model.Email, model.PhoneNumber, model.Password, triggered.Name);

            return await userStorage.AddAsync(user);
        }

        public async Task<int> UpdateAsync(User model, string triggeredBy)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            var triggred = await ValidatePermissionToOperation(RoleType.Administrator, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            var record = await userStorage.GetAsync(model.Id);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.Update(model.Name, model.Email, model.PhoneNumber, model.Role, triggred.Name);

            await ValidateUserAsync(record);

            return await userStorage.UpdateAsync(record);
        }

        public async Task<int> DeleteAsync(Guid id, string triggeredBy)
        {
            var triggred = await ValidatePermissionToOperation(RoleType.Administrator, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            return await userStorage.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(IEnumerable<Guid> ids, string triggeredBy)
        {
            var triggred = await ValidatePermissionToOperation(RoleType.Administrator, triggeredBy);

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

        public async Task<PagedDataDto<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchOptions, IEnumerable<SortOptions> sortOptions, PaginationOptions pagination)
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
    }
}
