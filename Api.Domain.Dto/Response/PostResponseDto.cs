namespace Api.Domain.Dto.Response;

public class PostResponseDto : BaseCatalogResponseDto
{
    public string? ImagePostUrl { get; set; }

    public DateTime PublicationDate { get; set; }

    public int UserAccountId { get; set; }

    public string? UserAccounName { get; set; }

    public string? FullName { get; set; }
}