using Domain;

namespace Tests;

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
    /// Tests returns correct sellers with distinct clients
    /// </summary>
    [Fact]
    public void GetSellersInPeriod_ReturnsCorrectSellers()
    {
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);

        var sellers = _fixture.Requests
            .Where(r => r.Type == RequestType.Sale)
            .Where(r => r.Date >= startDate && r.Date <= endDate)
            .Select(r => r.Client)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        var expectedCount = 3;
        var expectedSellerNames = new[]
        {
            "Ivanov Ivan Ivanovich",
            "Petrov Petr Petrovich",
            "Sidorov Alexey Sidorovich"
        };

        Assert.Equal(expectedCount, sellers.Count);
        Assert.Equal(expectedSellerNames[0], sellers[0].FullName);
        Assert.Equal(expectedSellerNames[1], sellers[1].FullName);
        Assert.Equal(expectedSellerNames[2], sellers[2].FullName);
    }

    /// <summary>
    /// Tests returns correct top clients for each request type
    /// </summary>
    [Fact]
    public void GetTopSellersByRequestCount_SeparateByType_ReturnsCorrectTop()
    {
        var topSellers = _fixture.Requests
            .Where(r => r.Type == RequestType.Sale)
            .GroupBy(r => r.Client)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        var topBuyers = _fixture.Requests
            .Where(r => r.Type == RequestType.Purchase)
            .GroupBy(r => r.Client)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        var expectedTopSellerCount = 1;
        var expectedTopSellerName = "Ivanov Ivan Ivanovich"; 
        var expectedTopBuyerCount = 1;
        var expectedTopBuyerName = "Markelov Rodion Sergeevich";

        Assert.Equal(expectedTopSellerCount, topSellers.First().Count);
        Assert.Equal(expectedTopSellerName, topSellers.First().Client.FullName);
        Assert.Equal(expectedTopBuyerCount, topBuyers.First().Count);
        Assert.Equal(expectedTopBuyerName, topBuyers.First().Client.FullName);
    }

    /// <summary>
    /// Tests returns correct counts for each property type
    /// </summary>
    [Fact]
    public void GetRequestCountByPropertyType_ReturnsCorrectCounts()
    {
        var counts = _fixture.Requests
            .GroupBy(r => r.Property.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        var expectedCounts = new Dictionary<PropertyType, int>
        {
            { PropertyType.Apartment, 3 },
            { PropertyType.House, 3 },
            { PropertyType.Office, 2 }
        };

        Assert.Equal(expectedCounts, counts);
    }

    /// <summary>
    /// Tests returns clients with minimum request amount
    /// </summary>
    [Fact]
    public void GetClientsWithMinAmountRequest_ReturnsCorrectClients()
    {
        var minAmount = _fixture.Requests.Min(r => r.Amount);

        var clients = _fixture.Requests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.Client)
            .DistinctBy(c => c.Id)
            .ToList();

        var expectedClientCount = 1;
        var expectedClientName = "Markelov Rodion Sergeevich";
        var expectedMinAmount = 4_500_000m;

        Assert.Equal(expectedClientCount, clients.Count);
        Assert.Equal(expectedClientName, clients[0].FullName);
        Assert.Equal(expectedMinAmount, minAmount);
    }

    /// <summary>
    /// Tests returns clients searching for specific property type ordered by name
    /// </summary>
    [Fact]
    public void GetClientsSearchingForPropertyType_OrderedByName_ReturnsCorrectList()
    {
        var propertyType = PropertyType.Apartment;

        var clients = _fixture.Requests
            .Where(r => r.Type == RequestType.Purchase && r.Property.Type == propertyType)
            .Select(r => r.Client)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        var expectedClientCount = 2;
        var expectedClientNames = new[]
        {
            "Markelov Rodion Sergeevich",
            "Smirnov Sergey Ivanovich"
        };

        Assert.Equal(expectedClientCount, clients.Count);
        Assert.Equal(expectedClientNames[0], clients[0].FullName);
        Assert.Equal(expectedClientNames[1], clients[1].FullName);
    }
}