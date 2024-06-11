using Api.Common.Interfaces.Entities;

namespace Api.Common.Entities;

public abstract class BaseRemovableAuditableEntity : BaseAuditableEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}