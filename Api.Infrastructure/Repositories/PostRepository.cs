using Microsoft.EntityFrameworkCore;

using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Infrastructure.Data;
using Api.Domain.Dto.QueryFilters;

namespace Api.Infrastructure.Repositories;

public class PostRepository : CatalogBaseRepository<Post>, IPostRepository
{
    public PostRepository(ApiDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Post>> GetPaged(Post entity)
    {
        var query = _dbContext.Post.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPaged(PostQueryFilter entity)
    {
        var query = _dbContext.Post.AsQueryable();

        return await query.ToListAsync();
    }
}