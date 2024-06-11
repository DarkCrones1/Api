using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common.Entities;

namespace Api.Common.Interfaces.Repositories;

public interface ICatalogBaseRepository<T> : ICrudRepository<T>, IQueryPagedRepository<T> where T : CatalogBaseEntity
{

}
