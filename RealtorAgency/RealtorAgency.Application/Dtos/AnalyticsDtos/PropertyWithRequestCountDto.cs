namespace RealtorAgency.Application.Dtos.AnalyticsDtos;
/// <summary>
/// Represents a dto for a property with request count.
/// </summary>
public class PropertyWithRequestCountDto
{
    /// <summary>
    /// Unique identifier of the property.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Type of the property.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Intended purpose of the property.
    /// </summary>
    public required string Purpose { get; set; }

    /// <summary>
    /// Cadastral number of the property.
    /// </summary>
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Physical address of the property.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Total area of the property in square meters.
    /// </summary>
    public required double TotalArea { get; set; }

    /// <summary>
    /// Number of requests for this property.
    /// </summary>
    public required int Count { get; set; } = 0;
}

