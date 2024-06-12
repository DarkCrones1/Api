using Api.Common.Entities;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;
using Api.Domain.Enumerations;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;

namespace Api.Application.Services;

public class CommentaryService : CrudService<Commentary>, ICommentaryService
{
    public CommentaryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Commentary>> GetPaged(CommentaryQueryFilter filter)
    {
        var result = await _unitOfWork.CommentaryRepository.GetPaged(filter);
        var pagedItems = PagedList<Commentary>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}