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

        return await query.ToListAsync();
    }
}