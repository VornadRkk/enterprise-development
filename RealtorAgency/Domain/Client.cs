namespace Domain;

/// <summary>
/// Represents a client (counterparty) interacting with the real estate agency.
/// </summary>
public class Client
{
    /// <summary>
    /// Gets or sets the unique identifier of the client.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the client.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the passport number of the client.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Gets or sets the contact phone number of the client.
    /// </summary>
    public required string ContactPhone { get; set; }
}