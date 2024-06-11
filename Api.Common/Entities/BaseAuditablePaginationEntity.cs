using System.ComponentModel.DataAnnotations.Schema;
using Api.Common.Interfaces.Entities;

namespace Api.Common.Entities;

public abstract class BaseAuditablePaginationEntity : BaseAuditableEntity, IPaginationQueryable
{
    [NotMapped]
    public int PageSize { get; set; }

    [NotMapped]
    public int PageNumber { get; set; }
}