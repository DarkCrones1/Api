namespace Api.Domain.Dto.Response;

public class PostDetailResponseDto : BaseCatalogResponseDto
{
    private IEnumerable<CommentaryResponseDto> _commentary;

    public PostDetailResponseDto()
    {
        _commentary = new List<CommentaryResponseDto>();
    }


    public string? ImagePostUrl { get; set; }

    public DateTime PublicationDate { get; set; }

    public int UserAccountId { get; set; }

    public string? UserInfoName { get; set; }

    public string? UserInfoProfilePictureUrl { get; set; }

    public IEnumerable<CommentaryResponseDto> Commentary { get => _commentary; set => _commentary = value; }
}