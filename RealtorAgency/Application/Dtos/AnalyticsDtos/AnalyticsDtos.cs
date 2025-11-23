namespace Application.Dtos.AnalyticsDtos;

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

/// <summary>
/// Represents a dto for a client with request count.
/// </summary>
public class ClientWithRequestCountDto
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

    /// <summary>
    /// Number of requests made by this client.
    /// </summary>
    public required int Count { get; set; } = 0;
}

/// <summary>
/// Represents a dto for a client with total request amount.
/// </summary>
public class ClientWithAmountDto
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

    /// <summary>
    /// Total or specific amount associated with client requests.
    /// </summary>
    public required decimal Amount { get; set; } = 0;
}

/// <summary>
/// Represents a dto for a property type with its request count.
/// </summary>
public class PropertyTypeDto
{
    /// <summary>
    /// Property type name.
    /// </summary>
    public required string PropertyType { get; set; }

    /// <summary>
    /// Number of requests for this property type.
    /// </summary>
    public required int Count { get; set; } = 0;
}

/// <summary>
/// Represents a dto for a property with request count.
/// </summary>
public class PropertyWithRequestCountDto
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
    /// Total area of the property in square meters.
    /// </summary>
    public required double TotalArea { get; set; }

    /// <summary>
    /// Number of requests for this property.
    /// </summary>
    public required int Count { get; set; } = 0;
}
