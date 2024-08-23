using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Repositories.Base;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class AccountRepository : BaseRepository<UserEntity, User>, IAccountStorage
    {
        public AccountRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }
    }
}
