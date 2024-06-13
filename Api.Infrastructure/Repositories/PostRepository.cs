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

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        if (entity.PublicationDate.HasValue)
            query = query.Where(x => x.PublicationDate == entity.PublicationDate.Value.Date);

        if (entity.PublicationDate.HasValue)
            query = query.Where(x => x.PublicationDate!.Value >= entity.MinPublicationDate!.Value.Date);

        if (entity.PublicationDate.HasValue)
            query = query.Where(x => x.PublicationDate!.Value >= entity.MaxPublicationDate!.Value.Date);

        if (entity.UserAccountId > 0)
            query = query.Where(x => x.UserAccountId == entity.UserAccountId);

        return await query.ToListAsync();
    }
}