using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Domain.TestData;

/// <summary>
/// Static test data for Client, Property, and Request entities used in seeding and unit tests.
/// </summary>
public static class TestData
{
    /// <summary>
    /// Gets static test data for Client entities.
    /// </summary>
    public static List<Client> GetClients() =>
    [
        new()
        {
            Id = 1,
            FullName = "Ivanov Ivan Ivanovich",
            PassportNumber = "1234 567890",
            ContactPhone = "+7 (900) 111-22-33"
        },
        new()
        {
            Id = 2,
            FullName = "Petrov Petr Petrovich",
            PassportNumber = "2345 678901",
            ContactPhone = "+7 (901) 222-33-44"
        },
        new()
        {
            Id = 3,
            FullName = "Sidorov Alexey Sidorovich",
            PassportNumber = "3456 789012",
            ContactPhone = "+7 (902) 333-44-55"
        },
        new()
        {
            Id = 4,
            FullName = "Markelov Rodion Sergeevich",
            PassportNumber = "4567 890123",
            ContactPhone = "+7 (903) 444-55-66"
        },
        new()
        {
            Id = 5,
            FullName = "Smirnov Sergey Ivanovich",
            PassportNumber = "5678 901234",
            ContactPhone = "+7 (904) 555-66-77"
        },
        new()
        {
            Id = 6,
            FullName = "Kuznetsov Dmitry Mikhailovich",
            PassportNumber = "6789 012345",
            ContactPhone = "+7 (905) 666-77-88"
        },
        new()
        {
            Id = 7,
            FullName = "Sokolov Viktor Nikolaevich",
            PassportNumber = "7890 123456",
            ContactPhone = "+7 (906) 777-88-99"
        },
        new()
        {
            Id = 8,
            FullName = "Lebedev Pavel Ivanovich",
            PassportNumber = "8901 234567",
            ContactPhone = "+7 (907) 888-99-00"
        },
        new()
        {
            Id = 9,
            FullName = "Volkov Andrey Petrovich",
            PassportNumber = "9012 345678",
            ContactPhone = "+7 (908) 999-00-11"
        },
        new()
        {
            Id = 10,
            FullName = "Morozov Konstantin Alexeevich",
            PassportNumber = "0123 456789",
            ContactPhone = "+7 (909) 000-11-22"
        }
    ];

    /// <summary>
    /// Gets static test data for Property entities.
    /// </summary>
    public static List<Property> GetProperties() =>
    [
        new()
        {
            Id = 1,
            Type = PropertyType.Apartment,
            Purpose = Purpose.Residential,
            CadastralNumber = "77:01:0000000:1234",
            Address = "г. Москва, ул. Тверская, д. 1",
            Floors = 5,
            TotalArea = 50,
            Rooms = 2,
            CeilingHeight = 2.7,
            FloorNumber = 3,
            HasEncumbrances = false
        },
        new()
        {
            Id = 2,
            Type = PropertyType.House,
            Purpose = Purpose.Residential,
            CadastralNumber = "50:24:0000000:5678",
            Address = "Московская обл., г. Одинцово, ул. Ленина, д. 10",
            Floors = 2,
            TotalArea = 120,
            Rooms = 4,
            CeilingHeight = 3.0,
            FloorNumber = 1,
            HasEncumbrances = true
        },
        new()
        {
            Id = 3,
            Type = PropertyType.Office,
            Purpose = Purpose.Commercial,
            CadastralNumber = "77:02:0000000:9012",
            Address = "г. Москва, Пресненская наб., д. 10",
            Floors = 10,
            TotalArea = 80,
            Rooms = 3,
            CeilingHeight = 2.9,
            FloorNumber = 7,
            HasEncumbrances = false
        },
        new()
        {
            Id = 4,
            Type = PropertyType.Apartment,
            Purpose = Purpose.Residential,
            CadastralNumber = "77:03:0000000:3456",
            Address = "г. Москва, ул. Арбат, д. 25",
            Floors = 8,
            TotalArea = 65,
            Rooms = 3,
            CeilingHeight = 2.8,
            FloorNumber = 5,
            HasEncumbrances = false
        },
        new()
        {
            Id = 5,
            Type = PropertyType.House,
            Purpose = Purpose.Residential,
            CadastralNumber = "50:25:0000000:7890",
            Address = "Московская обл., г. Подольск, ул. Советская, д. 50",
            Floors = 3,
            TotalArea = 200,
            Rooms = 6,
            CeilingHeight = 3.2,
            FloorNumber = 1,
            HasEncumbrances = false
        },
        new()
        {
            Id = 6,
            Type = PropertyType.Office,
            Purpose = Purpose.Commercial,
            CadastralNumber = "77:04:0000000:1111",
            Address = "г. Москва, ул. Садовая-Кудринская, д. 15",
            Floors = 7,
            TotalArea = 120,
            Rooms = 5,
            CeilingHeight = 2.9,
            FloorNumber = 4,
            HasEncumbrances = true
        },
        new()
        {
            Id = 7,
            Type = PropertyType.Apartment,
            Purpose = Purpose.Residential,
            CadastralNumber = "77:05:0000000:2222",
            Address = "г. Москва, ул. Большая Дмитровка, д. 7",
            Floors = 6,
            TotalArea = 55,
            Rooms = 2,
            CeilingHeight = 2.7,
            FloorNumber = 2,
            HasEncumbrances = false
        },
        new()
        {
            Id = 8,
            Type = PropertyType.Office,
            Purpose = Purpose.Commercial,
            CadastralNumber = "77:06:0000000:3333",
            Address = "г. Москва, Ленинградский пр., д. 80",
            Floors = 15,
            TotalArea = 150,
            Rooms = 8,
            CeilingHeight = 3.0,
            FloorNumber = 10,
            HasEncumbrances = false
        },
        new()
        {
            Id = 9,
            Type = PropertyType.House,
            Purpose = Purpose.Residential,
            CadastralNumber = "50:26:0000000:4444",
            Address = "Московская обл., д. Голицыно, ул. Советская, д. 100",
            Floors = 2,
            TotalArea = 180,
            Rooms = 5,
            CeilingHeight = 3.1,
            FloorNumber = 1,
            HasEncumbrances = true
        },
        new()
        {
            Id = 10,
            Type = PropertyType.Apartment,
            Purpose = Purpose.Residential,
            CadastralNumber = "77:07:0000000:5555",
            Address = "г. Москва, ул. Красная площадь, д. 3",
            Floors = 9,
            TotalArea = 75,
            Rooms = 3,
            CeilingHeight = 2.8,
            FloorNumber = 6,
            HasEncumbrances = false
        }
    ];

    /// <summary>
    /// Gets static test data for Request entities.
    /// </summary>
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
