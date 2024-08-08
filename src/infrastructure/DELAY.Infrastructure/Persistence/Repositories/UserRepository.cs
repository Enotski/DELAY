using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Enums;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.SelectOptions;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using DELAY.Core.Domain.Models.Base;
using DELAY.Infrastructure.Builders;
using DELAY.Infrastructure.Extensions;
using DELAY.Infrastructure.Persistence.Context;
using DELAY.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DELAY.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : NamedRepository<User>, IUserStorage
    {
        public UserRepository(DelayContext context) : base(context)
        {
        }

        private Expression<Func<User, bool>> BySearchOptionsSearchSpecification(IEnumerable<SearchOptions> options)
        {
            if (options is null || !options.Any())
            {
                return null;
            }

            Expression<Func<User, bool>> predicate = PredicateBuilder.True<User>();

            foreach (var opt in options)
            {
                if (opt == null || string.IsNullOrWhiteSpace(opt.Column) || string.IsNullOrWhiteSpace(opt.Value))
                {
                    continue;
                }

                var val = opt.Value.Trim().ToUpper();

                if(string.Equals(nameof(User.Name), opt.Column, StringComparison.OrdinalIgnoreCase))
                {
                    predicate = predicate.And(m => m.Name.ToUpper().Contains(val));
                }

                else if(string.Equals(nameof(User.Email), opt.Column, StringComparison.OrdinalIgnoreCase))
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

        private Func<IQueryable<User>, IOrderedQueryable<User>> BySortOptionsSortSpecification(IEnumerable<SortOptions> options)
        {
            if (options is null || !options.Any())
            {
                return null;
            }

            return (IQueryable<User> query) => {

                IOrderedQueryable<User> ordered = null;

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

                    else if (string.Equals(nameof(User.CreateDate), opt.Column, StringComparison.OrdinalIgnoreCase))
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
                }
                return ordered;
            };
        }

        public async Task<int> CountAsync(IEnumerable<SearchOptions> options, CancellationToken cancellationToken = default)
        {
            var filter = BySearchOptionsSearchSpecification(options);

            return await BuildQuery(filter).CountAsync(cancellationToken);
        }

        public Task<IEnumerable<User>> GetAssigedUsersToTicketAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetBoardUsersAsync(Guid boardId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedDataDto<User>> GetRecordsAsync(IEnumerable<SearchOptions> searchs, IEnumerable<SortOptions> sortsOptions, PaginationOptions pagination, CancellationToken cancellationToken = default)
        {
            var filter = BySearchOptionsSearchSpecification(searchs);
            var order = BySortOptionsSortSpecification(sortsOptions);

            var count = await CountAsync(filter);

            var records = await BuildQuery(filter, order, pagination).ToListAsync(cancellationToken);

            return new PagedDataDto<User>(count, records);
        }

        public async Task<bool> IsUniqueName(string name, Guid? id = null, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<User>(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if(id !=  null)
                filter = filter.And(x =>  x.Id != id);

            return !await BuildQuery(filter).AnyAsync(cancellationToken);
        }
        public async Task<bool> IsUniqueEmail(string email, Guid? id = null, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<User>(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (id != null)
                filter = filter.And(x => x.Id != id);

            return !await BuildQuery(filter).AnyAsync(cancellationToken);
        }
        public async Task<bool> IsUniquePhone(string phoneNumber, Guid? id = null, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<User>(x => x.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));

            if (id != null)
                filter = filter.And(x => x.Id != id);

            return !await BuildQuery(filter).AnyAsync(cancellationToken);
        }

        public async Task<KeyNamedModel> PermissionToPerformOperationAsync(RoleType role, string triggeredBy, CancellationToken cancellationToken = default)
        {
            var filter = PredicateBuilder.Create<User>(x => x.Name.Equals(triggeredBy, StringComparison.OrdinalIgnoreCase) && x.Role == role);
            var selector = KeyNamedSelectorSpecification();

            return await BuildQuery(filter).Select(selector).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
