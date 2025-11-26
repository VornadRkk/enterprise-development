using RealtorAgency.Domain.Enums;
namespace RealtorAgency.Domain.Entities;

/// <summary>
/// Represents a real estate property.
/// </summary>
public class Property
{
    /// <summary>
    /// Unique identifier of the property.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Type of the property.
    /// </summary>
    public required PropertyType Type { get; set; }

    /// <summary>
    /// Intended purpose of the property.
    /// </summary>
    public required Purpose Purpose { get; set; }

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