namespace Application.Dtos.RepositoryDtos;

// ==================== CLIENT DTOs ====================

/// <summary>
/// Represents a dto for a client.
/// </summary>
public class ClientGetDto
{
    /// <summary>
    /// Unique identifier of the client.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Full name of the client.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Passport number of the client.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Contact phone number of the client.
    /// </summary>
    public required string ContactPhone { get; set; }
}

/// <summary>
/// Represents a dto for a client that needed to create or edit.
/// </summary>
public class ClientEditDto
{
    /// <summary>
    /// Full name of the client.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Passport number of the client.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Contact phone number of the client.
    /// </summary>
    public required string ContactPhone { get; set; }
}

// ==================== PROPERTY DTOs ====================

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

/// <summary>
/// Represents a dto for a property that needed to create or edit.
/// </summary>
public class PropertyEditDto
{
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

// ==================== REQUEST DTOs ====================

/// <summary>
/// Represents a dto for a request.
/// </summary>
public class RequestGetDto
{
    /// <summary>
    /// Unique identifier of the request.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Identifier of the client associated with the request.
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Identifier of the property associated with the request.
    /// </summary>
    public required int PropertyId { get; set; }

    /// <summary>
    /// Type of the request (sale or purchase).
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Monetary amount of the request.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Date when the request was created.
    /// </summary>
    public required DateTime Date { get; set; }
}

/// <summary>
/// Represents a dto for a request that needed to create or edit.
/// </summary>
public class RequestEditDto
{
    /// <summary>
    /// Identifier of the client associated with the request.
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Identifier of the property associated with the request.
    /// </summary>
    public required int PropertyId { get; set; }

    /// <summary>
    /// Type of the request (sale or purchase).
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Monetary amount of the request.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Date when the request was created.
    /// </summary>
    public required DateTime Date { get; set; }
}
