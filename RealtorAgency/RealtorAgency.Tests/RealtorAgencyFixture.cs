using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.TestData;

namespace RealtorAgency.Tests;

public class RealtorAgencyFixture
{
    public static List<Client> Clients => TestData.Clients;
    public static List<Property> Properties => TestData.Properties;
    public List<Request> Requests => TestData.Requests;
}
