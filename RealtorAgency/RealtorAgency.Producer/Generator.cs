using Bogus;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Application.Dtos.RepositoryDtos;

namespace RealtorAgency.Producer;

/// <summary>
/// Data generator for realtor agency request contracts.
/// </summary>
public class Generator(int clientsCount, int propertiesCount)
{
    /// <summary>
    /// Faker generator for request edit contracts.
    /// </summary>
    private readonly Faker<RequestEditDto> _requestFaker = new Faker<RequestEditDto>()
        .RuleFor(x => x.ClientId, f => f.Random.Int(1, clientsCount))
        .RuleFor(x => x.PropertyId, f => f.Random.Int(1, propertiesCount))
        .RuleFor(x => x.Date, f => f.Date.Past(2).Date)
        .RuleFor(x => x.Type, f => f.PickRandom(
            RequestType.Sale.ToString(),
            RequestType.Purchase.ToString()
        ))
        .RuleFor(x => x.Amount, f => f.Random.Decimal(500_000, 50_000_000));

    /// <summary>
    /// Generates a new <see cref="RequestEditDto"/> contract instance.
    /// </summary>
    /// <returns>A new request contract.</returns>
    public RequestEditDto GenerateRequest() => _requestFaker.Generate();
}
