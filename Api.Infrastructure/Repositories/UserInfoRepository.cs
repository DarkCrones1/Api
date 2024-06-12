using Microsoft.EntityFrameworkCore;

using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Infrastructure.Data;
using Api.Domain.Dto.QueryFilters;

namespace Api.Infrastructure.Repositories;


public class UserInfoRepository : CrudRepository<UserInfo>, IUserInfoRepository
{
    public UserInfoRepository(ApiDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<UserInfo>> GetPaged(UserInfo entity)
    {
        var query = _dbContext.UserInfo.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserInfo>> GetPaged(UserInfoQueryFilter entity)
    {
        var query = _dbContext.UserInfo.AsQueryable();

        return await query.ToListAsync();
    }
}