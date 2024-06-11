using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Api.Common.Entities;
using Api.Common.Interfaces.Repositories;
using Api.Infrastructure.Data;
using Api.Common.Interfaces.Entities;

namespace Api.Infrastructure.Repositories;

public class RetrieveRepository<T> : IRetrieveRepository<T> where T : BaseQueryable
{
    protected readonly ApiDbContext _dbContext;
    protected readonly DbSet<T> _entity;

    public RetrieveRepository(ApiDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._entity = dbContext.Set<T>();
    }

    public virtual async Task<T> FirstOrDefault(int id)
    {
        var query = await _entity.FirstOrDefaultAsync(x => x.Id == id);
        return query!;
    }

    public virtual async Task<T> FirstOrDefaultBy(Expression<Func<T, bool>> filters)
    {
        var query = await _entity.FirstOrDefaultAsync(filters);
        return query!;
    }

    public virtual async Task<T> FirstOrDefaultBy(Expression<Func<T, bool>> filters, string includeProperties)
    {
        var query = await _entity.Include(includeProperties).FirstOrDefaultAsync(filters);
        return query!;
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? filters = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _entity;

        if (filters != null)
            query = query.Where(filters);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy != null)
            return orderBy(query).AsNoTracking().AsQueryable();

        return query.AsNoTracking().AsQueryable();
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        var query = await _entity.ToListAsync();
        return query;
    }

    public virtual async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filters, string includeProperties = "")
    {
        var query = _entity.Where(filters);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        return await query.ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        return await this.FirstOrDefault(id);
    }

    public virtual async Task<IEnumerable<T>> GetPaged(T entity)
    {
        var query = _entity.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id > 0);

        return await query.ToListAsync();
    }
}