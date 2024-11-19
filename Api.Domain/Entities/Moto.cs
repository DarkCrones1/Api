using Api.Common.Entities;

namespace Api.Domain.Entities;

public class Moto : CatalogBaseAuditablePaginationEntity
{
    public Guid Code { get; set; }

    public double CubicCentimeters { get; set; }

    public string Brand { get; set; } = null!;

    public short AvailableStatus {get; set;}

    public double Price {get; set;}

    public string ImageUrl { get; set; } = null!;

    public DateTime PublicationDate { get; set; }
}