using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.TestData;

namespace RealtorAgency.Tests;

public class RealtorAgencyFixture
{
    public static List<Client> Clients => TestData.GetClients();

    public static List<Property> Properties => TestData.GetProperties();

    public List<Request> Requests => TestData.GetRequests(Clients, Properties);
}
