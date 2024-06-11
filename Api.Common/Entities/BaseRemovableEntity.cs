using Api.Common.Interfaces.Entities;

namespace Api.Common.Entities;

public abstract class BaseRemovableEntity : BaseEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}