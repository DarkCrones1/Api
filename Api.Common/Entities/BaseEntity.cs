using Api.Common.Interfaces.Entities;

namespace Api.Common.Entities;

public abstract class BaseEntity : IBaseQueryable
{
    public int Id { get; set; }
}