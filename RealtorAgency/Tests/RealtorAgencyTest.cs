using Xunit;
using Domain;
using System.Linq;

namespace Tests;

/// <summary>
/// Contains unit tests for RealtorAgency domain logic
/// </summary>
public class RealtorAgencyTest
{
    /// <summary>
    /// Tests returns correct sellers with distinct clients
    /// </summary>
    [Fact]
    public void GetSellersInPeriod_ReturnsCorrectSellers()
    {
        var requests = RealtorAgencyFixture.CreateTestRequests();

        var sellers = requests
            .Where(r => r.Type == RequestType.Sale)
            .Select(r => r.Client)
            .DistinctBy(c => c.PassportNumber)
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(3, sellers.Count);
        Assert.Contains(sellers, c => c.FullName == "Иванов Иван Иванович");
        Assert.Contains(sellers, c => c.FullName == "Петров Пётр Петрович");
        Assert.Contains(sellers, c => c.FullName == "Сидоров Алексей Сергеевич");
    }

    /// <summary>
    /// Tests returns correct top clients for each request type
    /// </summary>
    [Fact]
    public void GetTop5ClientsByRequestCount_SeparateByType_ReturnsCorrectTop()
    {
        var requests = RealtorAgencyFixture.CreateTestRequests();

        var topSellers = requests
            .Where(r => r.Type == RequestType.Sale)
            .GroupBy(r => r.Client)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        var topBuyers = requests
            .Where(r => r.Type == RequestType.Purchase)
            .GroupBy(r => r.Client)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        Assert.Equal(2, topSellers.First().Count);
        Assert.Equal("Иванов Иван Иванович", topSellers.First().Client.FullName);

        Assert.Equal(3, topBuyers.First().Count);
        Assert.Equal("Кузнецов Дмитрий Андреевич", topBuyers.First().Client.FullName);
    }

    /// <summary>
    /// Tests  returns correct counts for each property type
    /// </summary>
    [Fact]
    public void GetRequestCountByPropertyType_ReturnsCorrectCounts()
    {
        var requests = RealtorAgencyFixture.CreateTestRequests();

        var counts = requests
            .GroupBy(r => r.Property.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(3, counts[PropertyType.Apartment]);
        Assert.Equal(3, counts[PropertyType.House]);
        Assert.Equal(2, counts[PropertyType.Office]);
    }

    /// <summary>
    /// Tests  returns clients with minimum request amount
    /// </summary>
    [Fact]
    public void GetClientsWithMinAmountRequest_ReturnsCorrectClients()
    {
        var requests = RealtorAgencyFixture.CreateTestRequests();
        var minAmount = RealtorAgencyFixture.GetMinAmount(requests);

        var clients = requests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.Client)
            .DistinctBy(c => c.PassportNumber)
            .ToList();

        Assert.Single(clients);
        Assert.Equal("Кузнецов Дмитрий Андреевич", clients[0].FullName);
        Assert.Equal(4_500_000m, minAmount);
    }

    /// <summary>
    /// Tests returns clients searching for specific property type ordered by name
    /// </summary>
    [Fact]
    public void GetClientsSearchingForPropertyType_OrderedByName_ReturnsCorrectList()
    {
        var requests = RealtorAgencyFixture.CreateTestRequests();
        var propertyType = PropertyType.Apartment;

        var clients = requests
            .Where(r => r.Type == RequestType.Purchase && r.Property.Type == propertyType)
            .Select(r => r.Client)
            .DistinctBy(c => c.PassportNumber)
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(2, clients.Count);
        Assert.Equal("Кузнецов Дмитрий Андреевич", clients[0].FullName);
        Assert.Equal("Смирнов Сергей Владимирович", clients[1].FullName);
    }
}