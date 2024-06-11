using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common.Entities;

namespace Api.Common.Interfaces.Services;

public interface ICatalogBaseService<T> : ICrudService<T> where T : CatalogBaseEntity
{
}