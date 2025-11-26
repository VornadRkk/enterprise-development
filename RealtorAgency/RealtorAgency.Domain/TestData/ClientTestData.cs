using RealtorAgency.Domain.Entities;

namespace RealtorAgency.Domain.TestData;
///
/// Static test data for Client entities used in seeding and unit tests.
///
public static class ClientTestData
{
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
}