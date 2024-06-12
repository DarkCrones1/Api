namespace Api.Domain.Dto.Request.Create;

public class UserAccountCreateRequestDto
{
    // UserAccount
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }
}