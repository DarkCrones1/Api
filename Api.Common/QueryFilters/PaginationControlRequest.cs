using Api.Common.Interfaces.Entities;

namespace Api.Common.QueryFilters;

public abstract class PaginationControlRequestFilter : IPaginationQueryable
{
    public int PageSize { get; set; } = 15;
    public int PageNumber { get; set; } = 1;
}