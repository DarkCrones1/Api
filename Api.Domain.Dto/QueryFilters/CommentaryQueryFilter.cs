using Api.Common.Interfaces.Entities;
using Api.Common.QueryFilters;

namespace Api.Domain.Dto.QueryFilters;

public class CommentaryQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public int UserAccountId { get; set; }

    public int PostId { get; set; }

    public string? Description {get; set;}

    public bool? IsDeleted { get; set; }
}