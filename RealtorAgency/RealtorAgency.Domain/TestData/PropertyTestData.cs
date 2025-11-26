using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Domain.TestData;
///
/// Static test data for Property entities used in seeding and unit tests.
///
public static class PropertyTestData
{
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
    {Id = 7,
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
}

