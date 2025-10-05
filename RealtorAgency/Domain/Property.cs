namespace Domain;

/// <summary>
/// Represents a real estate property.
/// </summary>
public class Property
{
    /// <summary>
    /// Gets or sets the unique identifier of the property.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the property.
    /// </summary>
    public required PropertyType Type { get; set; }

    /// <summary>
    /// Gets or sets the intended purpose of the property.
    /// </summary>
    public required Purpose Purpose { get; set; }

    /// <summary>
    /// Gets or sets the cadastral number of the property.
    /// </summary>
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Gets or sets the physical address of the property.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets the total number of floors in the building.
    /// </summary>
    public required int Floors { get; set; }

    /// <summary>
    /// Gets or sets the total area of the property in square meters.
    /// </summary>
    public required double TotalArea { get; set; }

    /// <summary>
    /// Gets or sets the number of rooms in the property.
    /// </summary>
    public required int Rooms { get; set; }

    /// <summary>
    /// Gets or sets the ceiling height in meters.
    /// </summary>
    public required double CeilingHeight { get; set; }

    /// <summary>
    /// Gets or sets the floor number on which the property is located.
    /// </summary>
    public required int FloorNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the property has legal encumbrances.
    /// </summary>
    public required bool HasEncumbrances { get; set; }
}