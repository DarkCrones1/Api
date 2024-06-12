using Api.Common.Entities;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface ICommentaryService : ICrudService<Commentary>
{
    Task<PagedList<Commentary>> GetPaged(CommentaryQueryFilter filter);
}