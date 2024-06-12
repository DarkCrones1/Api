namespace Api.Domain.Dto.Request.Create;

public class CommentaryCreateRequestDto
{
    public string Description { get; set; } = null!;

    public int PostId { get; set; }
}