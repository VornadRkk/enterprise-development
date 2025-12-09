using Microsoft.EntityFrameworkCore;
using RealtorAgency.Domain.TestData;

namespace RealtorAgency.Infrastructure.Persistence;

/// <summary>
/// Provides helper methods to seed the database with initial test data for clients, properties, and requests.
/// Uses static test data from Domain.TestData namespace.
/// Ensures that the primary key sequences are correctly set after inserting data.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the <see cref="Client"/> table with initial test data if it is empty.
    /// </summary>
    /// <param name="context">The database context used to access the Clients table.</param>
    public static async Task SeedClientsAsync(AppDbContext context)
    {
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(TestData.Clients);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seeds the <see cref="Property"/> table with initial test data if it is empty.
    /// </summary>
    /// <param name="context">The database context used to access the Properties table.</param>
    public static async Task SeedPropertiesAsync(AppDbContext context)
    {
        if (!context.Properties.Any())
        {
            context.Properties.AddRange(TestData.Properties);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seeds the <see cref="Request"/> table with initial test data if it is empty.
    /// </summary>
    /// <param name="context">The database context used to access the Requests table.</param>
    public static async Task SeedRequestsAsync(AppDbContext context)
    {
        if (!context.Requests.Any())
        {
            context.Requests.AddRange(TestData.Requests);
            await context.SaveChangesAsync();
        }
    }

}