using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Repositories;

public interface IQueryPagedRepository<T> where T : IBaseQueryable 
{
    Task<IEnumerable<T>> GetPaged(T entity);
}