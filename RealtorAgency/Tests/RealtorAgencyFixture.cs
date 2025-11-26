using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Domain.TestData;

namespace Tests;

public class RealtorAgencyFixture
{
    public static List<Client> Clients => ClientTestData.GetClients();

    public static List<Property> Properties => PropertyTestData.GetProperties();

    public List<Request> Requests => RequestTestData.GetRequests(Clients, Properties);
}
