using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.TestData;

namespace RealtorAgency.Tests;

public class RealtorAgencyFixture
{
    public static List<Client> Clients => TestData.Clients;
    public static List<Property> Properties => TestData.Properties;
    public List<Request> Requests { get; }

    public RealtorAgencyFixture()
    {
        Requests = [.. TestData.Requests];

        foreach (var r in Requests)
        {
            r.Client = Clients.First(c => c.Id == r.ClientId);
            r.Property = Properties.First(p => p.Id == r.PropertyId);
        }
    }
}
