namespace RealtorAgency.Application.Dtos.AnalyticsDtos;
/// <summary>
/// Represents a dto for a property type with its request count.
/// </summary>
public class PropertyTypeDto
{
    /// <summary>
    /// Property type name.
    /// </summary>
    public required string PropertyType { get; set; }

    /// <summary>
    /// Number of requests for this property type.
    /// </summary>
    public required int Count { get; set; } = 0;
}
