using RealtorAgency.Domain.Enums;
namespace RealtorAgency.Domain.Entities;

/// <summary>
/// Represents a client request to buy or sell a real estate property.
/// </summary>
public class Request
{
    /// <summary>
    /// Unique identifier of the request.
    /// </summary>
    public int Id { get; set; }
    public int ClientId { get; set; }

    /// <summary>
    /// Client associated with the request.
    /// </summary>
    public  Client? Client { get; set; }

    public int PropertyId { get; set; }

    /// <summary>
    /// Property associated with the request.
    /// </summary>
    public Property? Property { get; set; }

    /// <summary>
    /// Type of the request (sale or purchase).
    /// </summary>
    public required RequestType Type { get; set; }

    /// <summary>
    /// Monetary amount of the request.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Date when the request was created.
    /// </summary>
    public DateTime Date { get; set; }
}   