using Api.Domain.Dto.Interfaces;

namespace Api.Domain.Dto.Request.Create;

public class BaseCatalogCreateRequestDto : ICatalogBaseDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;
}