namespace Api.Domain.Dto.Response;

public class MotoResponseDto : BaseCatalogResponseDto
{
    public Guid Code { get; set; }

    public double CubicCentimeters { get; set; }

    public string Brand { get; set; } = null!;

    public short AvailableStatus {get; set;}

    public string AvailableStatusName {get; set; } = string.Empty;

    public double Price {get; set;}

    public string ImageUrl { get; set; } = null!;

    public DateTime PublicationDate { get; set; }
}