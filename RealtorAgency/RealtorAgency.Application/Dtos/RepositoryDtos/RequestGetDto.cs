namespace RealtorAgency.Application.Dtos.RepositoryDtos;

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
