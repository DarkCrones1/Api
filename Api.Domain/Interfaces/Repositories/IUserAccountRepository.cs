using System.Linq.Expressions;
using Api.Common.Interfaces.Repositories;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface IUserAccountRepository : IQueryPagedRepository<ActiveUserAccount>, ICrudRepository<UserAccount>, IQueryFilterPagedRepository<UserAccount, UserAccountQueryFilter>
{
    Task<ActiveUserAccount> GetUserAccount(int id);
    Task<ActiveUserAccount> GetUserAccountToLogin(Expression<Func<ActiveUserAccount, bool>> filters);

    Task<IEnumerable<UserAccount>> GetPagedUserInfo(UserAccountQueryFilter filter);
}