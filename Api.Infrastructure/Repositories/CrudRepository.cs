using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Api.Common.Entities;
using Api.Common.Interfaces.Repositories;
using Api.Infrastructure.Data;

namespace Api.Infrastructure;

public class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
{
    protected readonly ApiDbContext _dbContext;
    protected readonly DbSet<T> _entity;

    public CrudRepository(ApiDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._entity = dbContext.Set<T>();
    }

    public virtual async Task<int> Create(T entity)
    {
        var result = await _entity.AddAsync(entity);
        return entity.Id;
    }

    public virtual async Task CreateRange(IEnumerable<T> entities)
    {
        await _entity.AddRangeAsync(entities);
    }

    public virtual async Task<int> Delete(int id)
    {
        var result = await _entity.Where(x => x.Id == id).ExecuteDeleteAsync();
        return result;
    }

    public virtual async Task<int> DeleteBy(Expression<Func<T, bool>> filter)
    {
        var result = await _entity.Where(filter).ExecuteDeleteAsync();
        return result;
    }

    public virtual async Task<int> DeleteRange(IEnumerable<int> idList)
    {
        var result = await _entity.Where(x => idList.Contains(x.Id)).ExecuteDeleteAsync();
        return result;
    }

    public virtual async Task<bool> Exist(Expression<Func<T, bool>> filters)
    {
        var result = await _entity.AnyAsync(filters);
        return result;
    }

    public virtual async Task<T> FirstOrDefault(int id)
    {
        var result = await _entity.FirstOrDefaultAsync(x => x.Id == id);
        return result!;
    }

    public virtual async Task<T> FirstOrDefaultBy(Expression<Func<T, bool>> filters)
    {
        var result = await _entity.FirstOrDefaultAsync(filters);
        return result!;
    }

    public virtual async Task<T> FirstOrDefaultBy(Expression<Func<T, bool>> filters, string includeProperties = "")
    {
        IQueryable<T> query = _entity;

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        var result = await query.FirstOrDefaultAsync(filters);

        return result!;
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? filters = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _entity;

        if (filters != null)
            query = query.Where(filters);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy != null)
            return orderBy(query);

        return query;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await Get().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filters, string includeProperties = "")
    {
        var query = _entity.AsQueryable();

        query = query.Where<T>(filters);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        return await query.ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        var entity = await _entity.FirstOrDefaultAsync(x => x.Id == id);
        return entity!;
    }

    public virtual async Task<IEnumerable<T>> GetPaged(T entity)
    {
        var query = _entity.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        return await query.ToListAsync();
    }

    public virtual void Update(T entity)
    {
        _entity.Update(entity);
    }
}