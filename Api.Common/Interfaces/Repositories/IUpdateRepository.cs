using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Repositories;
public interface IUpdateRepository<T> where T : IBaseQueryable
{
    void Update(T entity);
}