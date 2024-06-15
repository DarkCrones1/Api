namespace Api.Domain.Dto.Response;

public class UserAccountDetailResponseDto
{

    private IEnumerable<CommentaryResponseDto> _commentary;
    private IEnumerable<PostResponseDto> _post;

    public UserAccountDetailResponseDto()
    {
        _commentary = new List<CommentaryResponseDto>();
        _post = new List<PostResponseDto>();
    }

    public int Id { get; set; }

    public int UserInfoId { get; set; }

    public string? UserInfoProfilePictureUrl { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string CellPhone { get; set; } = string.Empty;

    public string? GenderName { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public IEnumerable<CommentaryResponseDto> Commentary { get => _commentary; set => _commentary = value; }

    public IEnumerable<PostResponseDto> Post { get => _post; set => _post = value; }
}