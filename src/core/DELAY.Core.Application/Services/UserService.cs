using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Application.Contracts.Models.Request;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserStorage userStorage;

        private readonly IModelMapperService mapper;

        public UserService(IUserStorage userStorage, IModelMapperService mapper)
        {
            this.userStorage = userStorage ?? throw new ArgumentNullException(nameof(userStorage));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAsync(UserDto user)
        {
            try
            {
                var newUser = new User(user.Name);

                await userStorage.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateAsync(UserDto user)
        {
            try
            {
                var record = await userStorage.GetAsync(user.Id);

                if (record == null)
                {
                    throw new Exception("Record not found");
                }

                record.Update(user.Name);

                await userStorage.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, string triggeredBy)
        {
            try
            {
                await userStorage.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids, string triggeredBy)
        {
            try
            {
                await userStorage.DeleteAsync(ids);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                return await userStorage.GetKeyNameRecordsAsync(ids);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<KeyNameDto>> GetKeyNameRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, SortOptionsDto sortOption, PaginationOptionsDto pagination)
        {
            try
            {
                return await userStorage.GetKeyNameRecordsAsync(searchOptions, sortOption, pagination);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDto> GetRecordAsync(Guid id)
        {
            try
            {
                var result = await userStorage.GetAsync(id);

                return mapper.Map<UserDto>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<UserDto>> GetRecordsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var result = await userStorage.GetAsync(ids);

                return mapper.Map<IReadOnlyList<UserDto>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<UserDto>> GetRecordsAsync(IEnumerable<SearchOptionsDto> searchOptions, IEnumerable<SortOptionsDto> sortOptions, PaginationOptionsDto pagination)
        {
            try
            {
                var result = await userStorage.GetRecordsAsync(searchOptions, sortOptions, pagination);

                return mapper.Map<IReadOnlyList<UserDto>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAssigedUsersToTicketAsync(Guid ticketId)
        {
            try
            {
                var result = await userStorage.GetAssigedUsersToTicketAsync(ticketId);

                return mapper.Map<IReadOnlyList<UserDto>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetBoardUsersAsync(Guid boardId)
        {
            try
            {
                var result = await userStorage.GetBoardUsersAsync(boardId);

                return mapper.Map<IReadOnlyList<UserDto>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
