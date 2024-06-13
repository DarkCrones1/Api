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

    public async Task UpdatePost(int postId, string urlPost, string userName)
    {
        var lastEntity = await _unitOfWork.PostRepository.GetById(postId);

        lastEntity.ImagePostUrl = urlPost;
        lastEntity.LastModifiedDate = DateTime.UtcNow;
        lastEntity.LastModifiedBy = userName;

        await base.Update(lastEntity);
        await _unitOfWork.SaveChangesAsync();
    }
}