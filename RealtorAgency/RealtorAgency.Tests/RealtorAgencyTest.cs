using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Tests;

/// <summary>
/// Contains unit tests for RealtorAgency domain logic
/// </summary>
public class RealtorAgencyTest : IClassFixture<RealtorAgencyFixture>
{
    private readonly RealtorAgencyFixture _fixture;

    public RealtorAgencyTest(RealtorAgencyFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Tests returns correct sellers with distinct clients within date range
    /// </summary>
    [Fact]
    public void GetSellersInPeriod_ReturnsCorrectSellers()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 12, 31);

        var expectedNames = new[]
        {
            "Ivanov Ivan Ivanovich",
            "Kuznetsov Dmitry Mikhailovich",
            "Petrov Petr Petrovich",
            "Sidorov Alexey Sidorovich"
        };

        var sellers = _fixture.Requests
            .Where(r => r.Type == RequestType.Sale && r.Date >= start && r.Date <= end)
            .Select(r => r.Client!)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(expectedNames.Length, sellers.Count);
        Assert.Equal(expectedNames, sellers.Select(s => s.FullName));
    }

    /// <summary>
    /// Tests returns correct top clients for each request type
    /// </summary>
    [Fact]
    public void GetTopSellersByRequestCount_SeparateByType_ReturnsCorrectTop()
    {
        var topSellerExpectedName = "Ivanov Ivan Ivanovich";
        var topBuyerExpectedName = "Markelov Rodion Sergeevich";

        var topSeller = _fixture.Requests
            .Where(r => r.Type == RequestType.Sale)
            .GroupBy(r => r.Client!)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .First();

        var topBuyer = _fixture.Requests
            .Where(r => r.Type == RequestType.Purchase)
            .GroupBy(r => r.Client!)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .First();

        Assert.Equal(topSellerExpectedName, topSeller.Client.FullName);
        Assert.Equal(topBuyerExpectedName, topBuyer.Client.FullName);
    }

    /// <summary>
    /// Tests returns correct counts for each property type
    /// </summary>
    [Fact]
    public void GetRequestCountByPropertyType_ReturnsCorrectCounts()
    {
        var expected = new Dictionary<PropertyType, int>
        {
            { PropertyType.Apartment, 4 },
            { PropertyType.House, 4 },
            { PropertyType.Office, 2 }
        };

        var result = _fixture.Requests
            .GroupBy(r => r.Property!.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests returns clients with minimum request amount
    /// </summary>
    [Fact]
    public void GetClientsWithMinAmountRequest_ReturnsCorrectClients()
    {
        var expectedNames = new[] { "Markelov Rodion Sergeevich" };
        var expectedMinAmount = 4_500_000m;

        var minAmount = _fixture.Requests.Min(r => r.Amount);

        var clients = _fixture.Requests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.Client!)
            .DistinctBy(c => c.Id)
            .ToList();

        Assert.Single(clients);
        Assert.Equal(expectedNames, clients.Select(c => c.FullName));
        Assert.Equal(expectedMinAmount, minAmount);
    }

    /// <summary>
    /// Tests returns clients searching for specific property type ordered by name
    /// </summary>
    [Fact]
    public void GetClientsSearchingForPropertyType_OrderedByName_ReturnsCorrectList()
    {
        var propertyType = PropertyType.Apartment;

        var expectedNames = new[]
        {
            "Markelov Rodion Sergeevich",
            "Smirnov Sergey Ivanovich"
        };

        var clients = _fixture.Requests
            .Where(r => r.Type == RequestType.Purchase && r.Property!.Type == propertyType)
            .Select(r => r.Client!)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(expectedNames.Length, clients.Count);
        Assert.Equal(expectedNames, clients.Select(c => c.FullName));
    }
}
