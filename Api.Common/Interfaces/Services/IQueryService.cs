using System.Linq.Expressions;
using Api.Common.Entities;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Services;
public interface IQueryService<T> where T : IBaseQueryable
{
    /// <summary>
    /// Get all items
    /// </summary>
    /// <returns>List Items</returns>
    Task<IEnumerable<T>> GetAll();
    //Task<IEnumerable<T>> GetAll(BaseCatalogQueryFilter filters);
    /// <summary>
    /// Get item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetById(int id);
    //Task<IEnumerable<T>> GetBy<J>(T entity);
    /// <summary>
    /// Get items by filter conditions
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filters, string includeProperties = "");
    /// <summary>
    /// Get ordered items and relationshipments by filter conditions
    /// </summary>
    /// <param name="filters"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    IQueryable<T> Get(Expression<Func<T, bool>>? filters = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

    Task<PagedList<T>> GetPaged(T entity);
}
