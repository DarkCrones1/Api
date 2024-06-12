namespace Api.Domain.Dto.Response;

public class UserAccountResponseDto
{
    public int Id { get; set; }

    public int UserInfoId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string CellPhone { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }
}