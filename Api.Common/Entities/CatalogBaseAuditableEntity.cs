using Api.Common.Interfaces.Entities;

namespace Api.Common.Entities;

public abstract class CatalogBaseAuditableEntity : CatalogBaseEntity, IAuditableEntity
{
    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? LastModifiedBy { get; set; }
}