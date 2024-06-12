namespace AW.Domain.Dto.Request;

public class LoginRequestDto
{
    public string? UserNameOrEmail { get; set; }

    public string? Password { get; set; }
}