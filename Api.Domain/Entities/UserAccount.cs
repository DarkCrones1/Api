using Api.Common.Entities;

namespace Api.Domain.Entities;

public partial class UserAccount : BaseRemovablePaginationEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsAuthorized { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Commentary> Commentary { get; } = new List<Commentary>();

    public virtual ICollection<Post> Post { get; } = new List<Post>();

    public virtual ICollection<UserInfo> UserInfo { get; } = new List<UserInfo>();
}