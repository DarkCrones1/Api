using System.Linq.Expressions;
using Api.Common.Interfaces.Repositories;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface IPostRepository : ICatalogBaseRepository<Post>, IQueryFilterPagedRepository<Post, PostQueryFilter>
{
}