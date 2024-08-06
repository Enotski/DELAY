using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserStorage userStorage;

        private readonly IModelMapperService mapper;

        public UserService(IUserStorage userStorage, IModelMapperService mapper, ICryptographyService cryptoService)
        {
            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(IUserStorage));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(IModelMapperService));
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

        public async Task<Guid> AddAsync(User user, string triggeredBy)
        {
            var triggred = await ValidatePermissionToOperation(RoleType.Administrator, triggeredBy);

            if (!user.IsValid())
                throw new ArgumentException("Invalid user data");

            var unique = await userStorage.IsUnique(user.Name, user.Email, user.PhoneNumber);

            if(!unique)
                throw new ArgumentException("User with such name, email or phone number is already exist");

           return await userStorage.AddAsync(user);
        }

        public async Task<int> UpdateAsync(User user, string triggeredBy)
        {
            var triggred = await ValidatePermissionToOperation(RoleType.Administrator, triggeredBy);

            if (triggred == null)
            {
                throw new Exception("No permission for operation");
            }

            var record = await userStorage.GetAsync(user.Id);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.Update(user.Name, user.Email, user.PhoneNumber, triggred.Name);

            if (!record.IsValid())
                throw new ArgumentException("Invalid user data");

            var unique = await userStorage.IsUnique(record.Name, record.Email, record.PhoneNumber);

            if (!unique)
                throw new ArgumentException("User with such name, email or phone number is already exist");

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

        public async Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids)
        {
            return await userStorage.GetKeyNameRecordsAsync(ids);
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, SortOptionsDto sortOption, PaginationOptionsDto pagination)
        {
            return await userStorage.GetKeyNameRecordsAsync(searchOptions, sortOption, pagination);
        }

        public async Task<User> GetRecordAsync(Guid id)
        {
            var result = await userStorage.GetAsync(id);

            return mapper.Map<User>(result);
        }

        public async Task<IReadOnlyList<User>> GetRecordsAsync(IEnumerable<Guid> ids)
        {
            var result = await userStorage.GetAsync(ids);

            return mapper.Map<IReadOnlyList<User>>(result);
        }

        public async Task<IReadOnlyList<User>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto pagination)
        {
            var result = await userStorage.GetRecordsAsync(searchOptions, sortOptions, pagination);

            return mapper.Map<IReadOnlyList<User>>(result);
        }

        public async Task<IEnumerable<User>> GetAssigedUsersToTicketAsync(Guid ticketId)
        {
            var result = await userStorage.GetAssigedUsersToTicketAsync(ticketId);

            return mapper.Map<IReadOnlyList<User>>(result);
        }

        public async Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId)
        {
            var result = await userStorage.GetBoardUsersAsync(boardId);

            return mapper.Map<IReadOnlyList<User>>(result);
        }
    }
}
