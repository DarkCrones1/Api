namespace Api.Domain.Dto.Request.Create;

public class MotoCreateRequestDto : BaseCatalogCreateRequestDto
{
    public double CubicCentimeters { get; set; }

    public string Brand { get; set; } = null!;

    public double Price {get; set;}

    public string ImageUrl { get; set; } = null!;
}