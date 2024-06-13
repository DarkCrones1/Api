using Api.Common.Interfaces.Entities;
using Api.Common.QueryFilters;

namespace Api.Domain.Dto.QueryFilters;

public class PostQueryFilter : BaseCatalogQueryFilter
{
    public DateTime? PublicationDate { get; set; }

    public DateTime? MinPublicationDate { get; set; }

    public DateTime? MaxPublicationDate { get; set; }

    public int UserAccountId { get; set; }
}