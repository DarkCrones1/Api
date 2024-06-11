using Api.Domain.Dto.Interfaces;

namespace Api.Domain.Dto.Response;

public class BaseCatalogResponseDto : ICatalogBaseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;

    public string Status { get; set; } = string.Empty;

    public bool? IsActive { get; set; } = null;
}