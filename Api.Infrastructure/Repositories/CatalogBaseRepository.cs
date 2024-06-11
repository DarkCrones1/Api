using Microsoft.EntityFrameworkCore;

using Api.Common.Entities;
using Api.Common.Interfaces.Repositories;
using Api.Infrastructure.Data;

namespace Api.Infrastructure.Repositories;

public class CatalogBaseRepository<T> : CrudRepository<T>, ICatalogBaseRepository<T> where T : CatalogBaseEntity
{
    public CatalogBaseRepository(ApiDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<T>> GetPaged(T entity)
    {
        var query = _entity.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted.Value);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        return await query.ToListAsync();
    }
}