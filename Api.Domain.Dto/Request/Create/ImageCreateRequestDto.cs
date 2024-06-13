using Microsoft.AspNetCore.Http;

namespace Api.Domain.Dto.Request.Create;

public class ImageCreateRequestDto
{
    public int EntityAssigmentId { get; set; }

    public IFormFile File { get; set; } = null!;
}