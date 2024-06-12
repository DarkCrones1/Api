using System.Linq.Expressions;
using Api.Common.Entities;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IUserAccountService : ICrudService<UserAccount>
{
    Task<int> CreateUser(UserAccount user);
    Task<ActiveUserAccount> GetUserAccount(int id);
    Task<ActiveUserAccount> GetUserAccountToLogin(Expression<Func<ActiveUserAccount, bool>> filters);

    Task<PagedList<UserAccount>> GetPaged(UserAccountQueryFilter filter);
}