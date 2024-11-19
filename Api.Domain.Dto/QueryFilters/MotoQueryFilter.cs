using Api.Common.Interfaces.Entities;
using Api.Common.QueryFilters;

namespace Api.Domain.Dto.QueryFilters;

public partial class MotoQueryFilter : BaseCatalogQueryFilter
{
    public double? CubicCentimeters { get; set; }

    public string? Brand { get; set; } = null!;

    public short? AvailableStatus {get; set;}

    public double? Price {get; set;}
}