using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;
namespace RealtorAgency.Domain.TestData;
///
/// Static test data for Request entities used in seeding and unit tests.
///
public static class RequestTestData
{
    public static List<Request> GetRequests(List<Client> clients, List<Property> properties) =>
    [
    new()
    {
    Id = 1,
    Client = clients[0],
    Property = properties[0],
    Type = RequestType.Sale,
    Amount = 5_000_000,
    Date = new DateTime(2024, 1, 15)
    },
    new()
    {
    Id = 2,
    Client = clients[1],
    Property = properties[1],
    Type = RequestType.Sale,
    Amount = 15_000_000,
    Date = new DateTime(2024, 3, 20)
    },
    new()
    {
    Id = 3,
    Client = clients[2],
    Property = properties[2],
    Type = RequestType.Sale,
    Amount = 10_000_000,
    Date = new DateTime(2024, 5, 10)
    },
    new()
    {
    Id = 4,
    Client = clients[0],
    Property = properties[1],
    Type = RequestType.Sale,
    Amount = 14_000_000,
    Date = new DateTime(2024, 7, 5)
    },
    new()
    {
    Id = 5,
    Client = clients[3],
    Property = properties[0],
    Type = RequestType.Purchase,
    Amount = 4_500_000,
    Date = new DateTime(2024, 2, 12)
    },
    new()
    {
    Id = 6,
    Client = clients[4],
    Property = properties[0],
    Type = RequestType.Purchase,
    Amount = 4_800_000,
    Date = new DateTime(2024, 4, 18)
    },
    new()
    {
    Id = 7,
    Client = clients[3],
    Property = properties[2],
    Type = RequestType.Purchase,
    Amount = 9_500_000,
    Date = new DateTime(2024, 6, 22)
    },
    new()
    {
    Id = 8,
    Client = clients[3],
    Property = properties[1],
    Type = RequestType.Purchase,
    Amount = 14_200_000,
    Date = new DateTime(2024, 8, 30)
    },
    new()
    {
    Id = 9,
    Client = clients[5],
    Property = properties[3],
    Type = RequestType.Sale,
    Amount = 7_200_000,
    Date = new DateTime(2024, 9, 10)
    },
    new()
    {
    Id = 10,
    Client = clients[6],
    Property = properties[4],
    Type = RequestType.Purchase,
    Amount = 18_500_000,
    Date = new DateTime(2024, 10, 25)
    }
    ];
}
