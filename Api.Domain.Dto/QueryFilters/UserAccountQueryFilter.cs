using Api.Common.Interfaces.Entities;
using Api.Common.QueryFilters;

namespace Api.Domain.Dto.QueryFilters;

public class UserAccountQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public bool? IsDeleted { get; set; }
}