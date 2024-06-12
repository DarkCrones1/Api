using Api.Common.Entities;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IPostService : ICatalogBaseService<Post>
{
    Task<PagedList<Post>> GetPaged(PostQueryFilter filter);
}