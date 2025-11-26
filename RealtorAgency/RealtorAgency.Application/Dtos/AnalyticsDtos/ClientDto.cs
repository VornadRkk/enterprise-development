namespace RealtorAgency.Application.Dtos.AnalyticsDtos;

/// <summary>
/// Represents a dto for a client.
/// </summary>
public class ClientDto
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

