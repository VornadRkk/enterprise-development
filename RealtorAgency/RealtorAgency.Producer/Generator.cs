using Bogus;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Application.Dtos.RepositoryDtos;

namespace RealtorAgency.Producer;

/// <summary>
/// Data generator for realtor agency request contracts.
/// </summary>
public class Generator
{
    /// <summary>
    /// Faker generator for request edit contracts.
    /// </summary>
    private readonly Faker<RequestEditDto> _requestFaker;

    /// <summary>
    /// Clients count for generating valid ClientId references.
    /// </summary>
    private readonly int _clientsCount;

    /// <summary>
    /// Properties count for generating valid PropertyId references.
    /// </summary>
    private readonly int _propertiesCount;

    /// <summary>
    /// Initializes an instance of <see cref="Generator"/> and sets up Faker rules for request contracts.
    /// </summary>
    /// <param name="clientsCount">Total count of clients in database for generating valid ClientId.</param>
    /// <param name="propertiesCount">Total count of properties in database for generating valid PropertyId.</param>
    public Generator(int clientsCount, int propertiesCount)
    {
        _clientsCount = clientsCount;
        _propertiesCount = propertiesCount;
        _requestFaker = new Faker<RequestEditDto>()
            .RuleFor(x => x.ClientId, f => f.Random.Int(1, _clientsCount))
            .RuleFor(x => x.PropertyId, f => f.Random.Int(1, _propertiesCount))
            .RuleFor(x => x.Date, f => f.Date.Past(2).Date)
            .RuleFor(x => x.Type, f => f.PickRandom(
                RequestType.Sale.ToString(),
                RequestType.Purchase.ToString()
            ))
            .RuleFor(x => x.Amount, f => f.Random.Decimal(500_000, 50_000_000));
    }

    /// <summary>
    /// Generates a new <see cref="RequestEditDto"/> contract instance.
    /// </summary>
    /// <returns>A new request contract.</returns>
    public RequestEditDto GenerateRequest() => _requestFaker.Generate();
}
