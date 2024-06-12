using Api.Common.Entities;

namespace Api.Domain.Entities;

public partial class Post : CatalogBaseAuditablePaginationEntity
{
    public string? ImagePostUrl { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int UserAccountId { get; set; }

    public virtual UserAccount UserAccount { get; set; } = null!;

    public virtual ICollection<Commentary> Commentary { get; } = new List<Commentary>();
}