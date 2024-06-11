namespace Api.Common.QueryFilters;

/// <summary>
/// Class to retrieve common object from data base
/// </summary>
public class BaseCatalogQueryFilter : PaginationControlRequestFilter
{
    /// <summary>
    /// Get or Set Id property
    /// </summary>
    /// <value></value>
    public int? Id { get; set; }
    /// <summary>
    /// Get or Set Name property
    /// </summary>
    /// <value></value>
    public string? Name { get; set; } = null;
    /// <summary>
    /// Get or Set Description property
    /// </summary>
    /// <value></value>
    public string? Description { get; set; } = null;
    /// <summary>
    /// Get or Set IsDeleted property
    /// </summary>
    /// <value></value>
    public bool? IsDeleted { get; set; } = null;
}