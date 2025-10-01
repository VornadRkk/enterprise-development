namespace Domain;

/// <summary>
/// Represents a client request to buy or sell a real estate property.
/// </summary>
public class Request
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the client.
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Gets or sets the property.
    /// </summary>
    public required Property Property { get; set; }

    /// <summary>
    /// Gets or sets the type of the request.
    /// </summary>
    public required RequestType Type { get; set; }

    /// <summary>
    /// Gets or sets the monetary amount.
    /// </summary>
    public required decimal Amount { get; set; }
}