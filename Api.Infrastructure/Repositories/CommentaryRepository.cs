using Microsoft.EntityFrameworkCore;

using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Infrastructure.Data;
using Api.Domain.Dto.QueryFilters;

namespace Api.Infrastructure.Repositories;

public class CommentaryRepository : CrudRepository<Commentary>, ICommentaryRepository
{
    public CommentaryRepository(ApiDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Commentary>> GetPaged(Commentary entity)
    {
        var query = _dbContext.Commentary.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Commentary>> GetPaged(CommentaryQueryFilter entity)
    {
        var query = _dbContext.Commentary.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.UserAccountId > 0)
            query = query.Where(x => x.UserAccountId == entity.UserAccountId);

        if (entity.PostId > 0)
            query = query.Where(x => x.PostId == entity.PostId);

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        return await query.ToListAsync();
    }
}