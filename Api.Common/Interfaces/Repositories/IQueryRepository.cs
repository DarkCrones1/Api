using System.Linq.Expressions;
using Api.Common.Interfaces.Entities;
using Api.Common.QueryFilters;

namespace Api.Common.Interfaces.Repositories;

public interface IQueryRepository<T>  : IQueryExpresionFilterRepository<T>, IFirstOrDefaultRepository<T> where T : IBaseQueryable
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    //IEnumerable<T> GetBy(T entity);
}