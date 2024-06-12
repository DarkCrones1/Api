using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Infrastructure.Data;
using Api.Domain.Dto.QueryFilters;

namespace Api.Infrastructure.Repositories;

public class UserAccountRepository : CrudRepository<UserAccount> ,IUserAccountRepository
{
    public UserAccountRepository(ApiDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ActiveUserAccount>> GetPaged(ActiveUserAccount entity)
    {
        var query = _dbContext.ActiveUserAccount.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserAccount>> GetPaged(UserAccountQueryFilter entity)
    {
        var query = _dbContext.UserAccount.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.UserName) && !string.IsNullOrWhiteSpace(entity.UserName))
            query = query.Where(x => x.UserName.Contains(entity.UserName));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserAccount>> GetPagedUserInfo(UserAccountQueryFilter entity)
    {
        var query = _dbContext.UserAccount.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.UserName) && !string.IsNullOrWhiteSpace(entity.UserName))
            query = query.Where(x => x.UserName.Contains(entity.UserName));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        return await query.ToListAsync();
    }

    public async Task<ActiveUserAccount> GetUserAccount(int id)
    {
        Expression<Func<ActiveUserAccount, bool>> filter = x => x.Id == id;
        var entity = await GetUserAccountToLogin(filter);

        return entity ?? new ActiveUserAccount();
    }

    public async Task<ActiveUserAccount> GetUserAccountToLogin(Expression<Func<ActiveUserAccount, bool>> filters)
    {
        var entity = await _dbContext.ActiveUserAccount
        .Where(filters)
        .AsNoTracking()
        .FirstOrDefaultAsync();

        return entity ?? new ActiveUserAccount();
    }
}