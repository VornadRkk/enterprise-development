using Domain.Entities;
using Domain.Enums;

namespace Tests;

public class RealtorAgencyFixture
{
    public static List<Client> Clients =>
    [
        new Client
        {
            Id = 1,
            FullName = "Ivanov Ivan Ivanovich",
            PassportNumber = "1234 567890",
            ContactPhone = "+7 (900) 111-22-33"
        },
        new Client
        {
            Id = 2,
            FullName = "Petrov Petr Petrovich",
            PassportNumber = "2345 678901",
            ContactPhone = "+7 (901) 222-33-44"
        },
        new Client
        {
            Id = 3,
            FullName = "Sidorov Alexey Sidorovich",
            PassportNumber = "3456 789012",
            ContactPhone = "+7 (902) 333-44-55"
        },
        new Client
        {
            Id = 4,
            FullName = "Markelov Rodion Sergeevich",
            PassportNumber = "4567 890123",
            ContactPhone = "+7 (903) 444-55-66"
        },
        new Client
        {
            Id = 5,
            FullName = "Smirnov Sergey Ivanovich",
            PassportNumber = "5678 901234",
            ContactPhone = "+7 (904) 555-66-77"
        }
    ];

    public List<Property> Properties =>
    [
        new Property
        {
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
        new Property
        {
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
        new Property
        {
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
        }
    ];

    public List<Request> Requests =>
    [
        new Request
        {
            Client = Clients[0],
            Property = Properties[0],
            Type = RequestType.Sale,
            Amount = 5_000_000,
            Id = 1,
            Date = new DateTime(2024, 1, 15)
        },
        new Request
        {
            Client = Clients[1],
            Property = Properties[1],
            Type = RequestType.Sale,
            Amount = 15_000_000,
            Id = 2,
            Date = new DateTime(2024, 3, 20)
        },
        new Request
        {
            Client = Clients[2],
            Property = Properties[2],
            Type = RequestType.Sale,
            Amount = 10_000_000,
            Id = 3,
            Date = new DateTime(2024, 5, 10)
        },
        new Request
        {
            Client = Clients[0],
            Property = Properties[1],
            Type = RequestType.Sale,
            Amount = 14_000_000,
            Id = 4,
            Date = new DateTime(2024, 7, 5)
        },
        new Request
        {
            Client = Clients[3],
            Property = Properties[0],
            Type = RequestType.Purchase,
            Amount = 4_500_000,
            Id = 5,
            Date = new DateTime(2024, 2, 12)
        },
        new Request
        {
            Client = Clients[4],
            Property = Properties[0],
            Type = RequestType.Purchase,
            Amount = 4_800_000,
            Id = 6,
            Date = new DateTime(2024, 4, 18)
        },
        new Request
        {
            Client = Clients[3],
            Property = Properties[2],
            Type = RequestType.Purchase,
            Amount = 9_500_000,
            Id = 7,
            Date = new DateTime(2024, 6, 22)
        },
        new Request
        {
            Client = Clients[3],
            Property = Properties[1],
            Type = RequestType.Purchase,
            Amount = 14_200_000,
            Id = 8,
            Date = new DateTime(2024, 8, 30)
        }
    ];
}