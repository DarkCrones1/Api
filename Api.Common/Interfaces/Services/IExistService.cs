using System.Linq.Expressions;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Services;

    public interface IExistService<T> where T : IBaseQueryable
    {
        Task<bool> Exist(Expression<Func<T, bool>> filters);
    }
