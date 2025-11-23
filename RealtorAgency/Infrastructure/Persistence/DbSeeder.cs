using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence;

/// <summary>
/// Provides helper methods to seed the database with initial test data for clients, properties, and requests.
/// Ensures that the primary key sequences are correctly set after inserting data.
/// </summary>
public static class DbSeeder
{
    private static List<Client> ClientsData =>
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
        }
    ];

    private static List<Property> PropertiesData =>
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
        }
    ];

    /// <summary>
    /// Seeds the <see cref="Client"/> table with initial test data if it is empty.
    /// After inserting, updates the primary key sequence to match the maximum existing ID.
    /// </summary>
    /// <param name="context">The database context used to access the Clients table.</param>
    public static async Task SeedClientsAsync(AppDbContext context)
    {
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(ClientsData);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seeds the <see cref="Property"/> table with initial test data if it is empty.
    /// After inserting, updates the primary key sequence to match the maximum existing ID.
    /// </summary>
    /// <param name="context">The database context used to access the Properties table.</param>
    public static async Task SeedPropertiesAsync(AppDbContext context)
    {
        if (!context.Properties.Any())
        {
            context.Properties.AddRange(PropertiesData);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seeds the <see cref="Request"/> table with initial test data if it is empty.
    /// After inserting, updates the primary key sequence to match the maximum existing ID.
    /// </summary>
    /// <param name="context">The database context used to access the Requests table.</param>
    public static async Task SeedRequestsAsync(AppDbContext context)
    {
        if (!context.Requests.Any())
        {
            var clients = await context.Clients.OrderBy(c => c.Id).ToListAsync();
            var properties = await context.Properties.OrderBy(p => p.Id).ToListAsync();

            List<Request> requestsData =
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
                }
            ];

            context.Requests.AddRange(requestsData);
            await context.SaveChangesAsync();
        }
    }
}
