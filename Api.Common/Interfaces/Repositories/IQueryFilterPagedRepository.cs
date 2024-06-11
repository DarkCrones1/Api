using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Repositories;

public interface IQueryFilterPagedRepository<T, E> : IQueryPagedRepository<T> where T : IBaseQueryable where E : IBaseQueryFilter
{
    Task<IEnumerable<T>> GetPaged(E filter);
}