using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Interfaces.Repositories;

public interface IRetrieveRepository<T> : IQueryRepository<T>, IQueryPagedRepository<T> where T : IBaseQueryable
{

}