using Api.Common.Entities;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;
using Api.Domain.Enumerations;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;

namespace Api.Application.Services;

public class PostService : CatalogBaseService<Post>, IPostService
{
    public PostService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Post>> GetPaged(PostQueryFilter filter)
    {
        var result = await _unitOfWork.PostRepository.GetPaged(filter);
        var pagedItems = PagedList<Post>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    
}