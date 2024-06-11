using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Services;
public interface IUpdateService<T> where T : IBaseQueryable
{
    Task Update(T entity);
}