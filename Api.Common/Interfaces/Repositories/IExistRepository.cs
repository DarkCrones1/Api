using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common.Entities;
using System.Linq.Expressions;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Repositories;

    public interface IExistRepository<T> where T : IBaseQueryable
    {
        Task<bool> Exist(Expression<Func<T, bool>> filters);
    }
