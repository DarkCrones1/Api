namespace Api.Domain.Dto.Response;

public class CommentaryResponseDto
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int UserAccountId { get; set; }

    public string? UserInfoName { get; set; }

    public int PostId { get; set; }

    public string PostName { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }


}