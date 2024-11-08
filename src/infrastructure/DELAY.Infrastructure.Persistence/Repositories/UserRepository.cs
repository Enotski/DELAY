using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Enums;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Application.Extensions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Builders;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Entities;
using DELAY.Infrastructure.Persistence.Extensions;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DELAY.Infrastructure.Persistence.Repositories
{

    internal class UserRepository : NamedRepository<UserEntity, User>, IUserStorage
    {
        public UserRepository(DelayContext context, IModelMapperService mapper) : base(context, mapper)
        {
        }

        private Expression<Func<UserEntity, bool>> BySearchOptionsSearchSpecification(IEnumerable<SearchOptions> options)
        {
            if (options is null || !options.Any())
            {
                return null;
            }

            Expression<Func<UserEntity, bool>> predicate = PredicateBuilder.True<UserEntity>();

            foreach (var opt in options)
            {
                if (opt == null || string.IsNullOrWhiteSpace(opt.Column) || string.IsNullOrWhiteSpace(opt.Value))
                {
                    continue;
                }

                var val = opt.Value.Trim().ToUpper();

                if (string.Equals(nameof(User.Name), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    predicate = predicate.And(m => m.Name.ToUpper().Contains(val));
                }

                else if (string.Equals(nameof(User.Email), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    predicate = predicate.And(m => m.Email.ToUpper().Contains(val));
                }

                else if (string.Equals(nameof(User.PhoneNumber), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    predicate = predicate.And(m => m.PhoneNumber.Contains(val));
                }

                else if (string.Equals(nameof(User.ChangedBy), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    predicate = predicate.And(m => m.ChangedBy.Contains(val));
                }

                else if (string.Equals(nameof(User.CreateDate), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    var interval = val.SliceCouple();

                    predicate = predicate.WhereDateBetween(x => x.CreateDate, interval[0], interval[1]);
                }

                else if (string.Equals(nameof(User.ChangedDate), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    var interval = val.SliceCouple();

                    predicate = predicate.WhereDateBetween(x => x.ChangedDate, interval[0], interval[1]);
                }
            }
            return predicate;
        }

        private Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>> BySortOptionsSortSpecification(IEnumerable<SortOptions> options)
        {
            if (options is null || !options.Any())
            {
                return null;
            }

            return (IQueryable<UserEntity> query) =>
            {

                IOrderedQueryable<UserEntity> ordered = null;

                foreach (var opt in options)
                {
                    if (string.Equals(nameof(User.Name), opt.Column, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ordered == null)
                        {
                            ordered = opt.Order == OrderType.Asc ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name);
                        }
                        else
                        {
                            ordered = opt.Order == OrderType.Asc ? ordered.ThenBy(m => m.Name) : ordered.ThenByDescending(m => m.Name);
                        }
                    }
                    else if (string.Equals(nameof(User.Email), opt.Column, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ordered == null)
                        {
                            ordered = opt.Order == OrderType.Asc ? query.OrderBy(m => m.Email) : query.OrderByDescending(m => m.Email);
                        }
                        else
                        {
                            ordered = opt.Order == OrderType.Asc ? ordered.ThenBy(m => m.Email) : ordered.ThenByDescending(m => m.Email);
                        }
                    }
                    else if (string.Equals(nameof(User.PhoneNumber), opt.Column, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ordered == null)
                        {
                            ordered = opt.Order == OrderType.Asc ? query.OrderBy(m => m.PhoneNumber) : query.OrderByDescending(m => m.PhoneNumber);
                        }
                        else
                        {
                            ordered = opt.Order == OrderType.Asc ? ordered.ThenBy(m => m.PhoneNumber) : ordered.ThenByDescending(m => m.PhoneNumber);
                        }
                    }
                    else if (string.Equals(nameof(User.ChangedBy), opt.Column, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ordered == null)
                        {
                            ordered = opt.Order == OrderType.Asc ? query.OrderBy(m => m.ChangedBy) : query.OrderByDescending(m => m.ChangedBy);
                        }
                        else
                        {
                            ordered = opt.Order == OrderType.Asc ? ordered.ThenBy(m => m.ChangedBy) : ordered.ThenByDescending(m => m.ChangedBy);
                        }
                    }
                    else if (string.Equals(nameof(User.ChangedDate), opt.Column, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ordered == null)
                        {
                            ordered = opt.Order == OrderType.Asc ? query.OrderBy(m => m.ChangedDate) : query.OrderByDescending(m => m.ChangedDate);
                        }
                        else
                        {
                            ordered = opt.Order == OrderType.Asc ? ordered.ThenBy(m => m.ChangedDate) : ordered.ThenByDescending(m => m.ChangedDate);
                        }
                    }
                    else
                    {
                        if (ordered == null)
                        {
                            ordered = opt.Order == OrderType.Asc ? query.OrderBy(m => m.CreateDate) : query.OrderByDescending(m => m.CreateDate);
                        }
                        else
                        {
                            ordered = opt.Order == OrderType.Asc ? ordered.ThenBy(m => m.CreateDate) : ordered.ThenByDescending(m => m.CreateDate);
                        }
                    }
                }
                return ordered;
            };
        }

        public async Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default)
        {
            var filter = BySearchOptionsSearchSpecification(options);

            return await BuildQuery(filter).CountAsync(cancellationToken);
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            email = email.ToUpperTrim();

            var user = await BuildQuery(x => x.Email.Trim().ToUpper() == email).FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<User>(user);
        }

        public async Task<User> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default)
        {
            phone = phone.Trim();

            var user = await BuildQuery(x => x.PhoneNumber.Trim() == phone).FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<User>(user);
        }

        public Task<IEnumerable<User>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedData<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchs, IEnumerable<SortOptions> sortsOptions, PaginationOptions pagination, CancellationToken cancellationToken = default)
        {
            var filter = BySearchOptionsSearchSpecification(searchs);
            var order = BySortOptionsSortSpecification(sortsOptions);

            var count = await CountAsync(filter);

            var records = await BuildQuery(filter, order, pagination).ToListAsync(cancellationToken);

            return new PagedData<User>(count, _mapper.Map<IEnumerable<User>>(records));
        }

        public async Task<bool> IsUniqueName(string name, Guid? id = null, CancellationToken cancellationToken = default)
        {
            name = name.ToUpperTrim();

            var filter = PredicateBuilder.Create<UserEntity>(x => x.Name.Trim().ToUpper() == name);

            if (id != null)
                filter = filter.And(x => x.Id != id);

            return !await Set.AnyAsync(filter, cancellationToken);
        }
        public async Task<bool> IsUniqueEmail(string email, Guid? id = null, CancellationToken cancellationToken = default)
        {
            email = email.ToUpperTrim();

            var filter = PredicateBuilder.Create<UserEntity>(x => x.Email.Trim().ToUpper() == email);

            if (id != null)
                filter = filter.And(x => x.Id != id);

            return !await Set.AnyAsync(filter, cancellationToken);
        }
        public async Task<bool> IsUniquePhone(string phoneNumber, Guid? id = null, CancellationToken cancellationToken = default)
        {
            phoneNumber = phoneNumber.ToUpperTrim();

            var filter = PredicateBuilder.Create<UserEntity>(x => x.PhoneNumber == phoneNumber);

            if (id != null)
                filter = filter.And(x => x.Id != id);

            return !await Set.AnyAsync(filter, cancellationToken);
        }

        public async Task<bool> IsAllowToPerformOperationAsync(RoleType role, Guid triggeredById, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<UserEntity>(x => x.Id == triggeredById && x.Role >= role);

            return await Set.AnyAsync(filter, cancellationToken);
        }
    }
}
