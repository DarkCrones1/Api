using Api.Common.Entities;

namespace Api.Domain.Entities;

public partial class UserInfo : BaseRemovableAuditablePaginationEntity
{
    public Guid Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string CellPhone { get; set; } = null!;

    public string? Phone { get; set; }

    public short? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}