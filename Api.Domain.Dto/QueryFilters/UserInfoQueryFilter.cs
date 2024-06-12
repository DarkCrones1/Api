using Api.Common.Interfaces.Entities;
using Api.Common.QueryFilters;

namespace Api.Domain.Dto.QueryFilters;

public class UserInfoQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string? Name { get; set; } = null!;

    public string? CellPhone { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? IsDeleted { get; set; }
}