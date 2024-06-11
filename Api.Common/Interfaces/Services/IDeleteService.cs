using System.Linq.Expressions;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Services;
public interface IDeleteService<T> where T : IBaseQueryable
{
    Task<int> Delete(int id);
    Task<int> DeleteRange(IEnumerable<int> idList);
    Task<int> DeleteBy(Expression<Func<T, bool>> filter);
}
