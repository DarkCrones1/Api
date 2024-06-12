using Api.Common.Entities;

namespace Api.Domain.Entities;

public partial class Commentary : BaseRemovableAuditablePaginationEntity
{
    public string Description { get; set; } = null!;

    public int UserAccountId { get; set; }

    public int PostId { get; set; }

    public virtual UserAccount UserAccount { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}