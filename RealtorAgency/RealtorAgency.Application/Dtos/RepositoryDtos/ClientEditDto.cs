namespace RealtorAgency.Application.Dtos.RepositoryDtos;
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
