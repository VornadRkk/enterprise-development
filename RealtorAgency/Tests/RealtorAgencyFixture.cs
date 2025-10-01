using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests;

/// <summary>
/// Contains test data and helper methods for unit tests.
/// </summary>
public static class RealtorAgencyFixture
{
    /// <summary>
    /// Creates a set of test requests with clients and properties using Russian-style passport numbers and phone numbers.
    /// </summary>
    public static Request[] CreateTestRequests()
    {
        var clients = new[]
        {
            new Client { FullName = "Ivanov Ivan Ivanovich", PassportNumber = "1234 567890", ContactPhone = "+7 (900) 111-22-33" },
            new Client { FullName = "Petrov Petr Petrovich", PassportNumber = "2345 678901", ContactPhone = "+7 (901) 222-33-44" },
            new Client { FullName = "Sidorov Alexey Sidorovich", PassportNumber = "3456 789012", ContactPhone = "+7 (902) 333-44-55" },
            new Client { FullName = "Markelov Rodion Sergeevich", PassportNumber = "4567 890123", ContactPhone = "+7 (903) 444-55-66" },
            new Client { FullName = "Smirnov Sergey Ivanovich", PassportNumber = "5678 901234", ContactPhone = "+7 (904) 555-66-77" }
        };

        var properties = new[]
        {
            new Property { Type = PropertyType.Apartment, Purpose = Purpose.Residential, CadastreNumber = "77:01:0000000:1234", Address = "г. Москва, ул. Тверская, д. 1", Floors = 5, TotalArea = 50, Rooms = 2, CeilingHeight = 2.7, FloorNumber = 3, HasEncumbrances = false },
            new Property { Type = PropertyType.House, Purpose = Purpose.Residential, CadastreNumber = "50:24:0000000:5678", Address = "Московская обл., г. Одинцово, ул. Ленина, д. 10", Floors = 2, TotalArea = 120, Rooms = 4, CeilingHeight = 3.0, FloorNumber = 1, HasEncumbrances = true },
            new Property { Type = PropertyType.Office, Purpose = Purpose.Commercial, CadastreNumber = "77:02:0000000:9012", Address = "г. Москва, Пресненская наб., д. 10", Floors = 10, TotalArea = 80, Rooms = 3, CeilingHeight = 2.9, FloorNumber = 7, HasEncumbrances = false }
        };

        return new[]
        {
            
            new Request { Client = clients[0], Property = properties[0], Type = RequestType.Sale, Amount = 5_000_000, Id = 1 },
            new Request { Client = clients[1], Property = properties[1], Type = RequestType.Sale, Amount = 15_000_000, Id = 2 },
            new Request { Client = clients[2], Property = properties[2], Type = RequestType.Sale, Amount = 10_000_000, Id = 3 },
            new Request { Client = clients[0], Property = properties[1], Type = RequestType.Sale, Amount = 14_000_000, Id = 4 }, 

            
            new Request { Client = clients[3], Property = properties[0], Type = RequestType.Purchase, Amount = 4_500_000, Id = 5 },
            new Request { Client = clients[4], Property = properties[0], Type = RequestType.Purchase, Amount = 4_800_000, Id = 6 },
            new Request { Client = clients[3], Property = properties[2], Type = RequestType.Purchase, Amount = 9_500_000, Id = 7 },
            new Request { Client = clients[3], Property = properties[1], Type = RequestType.Purchase, Amount = 14_200_000, Id = 8 }
        };
    }

    /// <summary>
    /// Returns the minimum amount among all requests.
    /// </summary>
    public static decimal GetMinAmount(Request[] requests)
    {
        return requests.Min(r => r.Amount);
    }

    /// <summary>
    /// Returns unique clients by passport number.
    /// </summary>
    public static IEnumerable<Client> DistinctClientsByPassport(IEnumerable<Client> clients)
    {
        return clients.DistinctBy(c => c.PassportNumber);
    }
}