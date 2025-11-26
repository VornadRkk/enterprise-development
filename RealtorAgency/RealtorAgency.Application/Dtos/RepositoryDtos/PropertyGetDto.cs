namespace RealtorAgency.Application.Dtos.RepositoryDtos;
/// <summary>
/// Represents a dto for a property.
/// </summary>
public class PropertyGetDto
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
    /// Total number of floors in the building.
    /// </summary>
    public required int Floors { get; set; }

    /// <summary>
    /// Total area of the property in square meters.
    /// </summary>
    public required double TotalArea { get; set; }

    /// <summary>
    /// Number of rooms in the property.
    /// </summary>
    public required int Rooms { get; set; }

    /// <summary>
    /// Ceiling height in meters.
    /// </summary>
    public required double CeilingHeight { get; set; }

    /// <summary>
    /// Floor number on which the property is located.
    /// </summary>
    public required int FloorNumber { get; set; }

    /// <summary>
    /// Indicates whether the property has legal encumbrances.
    /// </summary>
    public required bool HasEncumbrances { get; set; }
}
