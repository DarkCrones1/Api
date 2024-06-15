using Microsoft.AspNetCore.Http;

namespace Api.Domain.Dto.Request.Create;

public class ImageProfileCreateRequestDto
{
    public IFormFile File { get; set; } = null!;
}