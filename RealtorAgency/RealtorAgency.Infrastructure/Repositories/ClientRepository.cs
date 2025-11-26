using Microsoft.EntityFrameworkCore;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;
using RealtorAgency.Infrastructure.Persistence;

namespace RealtorAgency.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Client entities.
/// Provides CRUD methods for clients.
/// </summary>
/// <param name="context">The application's database context used for data access.</param>
public class ClientRepository(AppDbContext context) : IRepository<Client>
{
    /// <summary>
    /// Gets all clients.
    /// </summary>
    public async Task<IEnumerable<Client>> GetAllAsync() =>
        await context.Clients.ToListAsync();

    /// <summary>
    /// Gets a client by its ID.
    /// </summary>
    /// <param name="id">Client ID.</param>
    /// <returns>The <see cref="Client"/> if found; otherwise, null.</returns>
    public async Task<Client?> GetByIdAsync(int id) =>
        await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Checks if a client exists by ID.
    /// </summary>
    /// <param name="id">Client ID.</param>
    /// <returns>True if exists; otherwise, false.</returns>
    public async Task<bool> ExistsById(int id) =>
        await context.Clients.AnyAsync(c => c.Id == id);

    /// <summary>
    /// Adds a new client.
    /// </summary>
    /// <param name="client">Client to add.</param>
    public async Task AddAsync(Client client)
    {
        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing client.
    /// </summary>
    /// <param name="client">Client with updated data.</param>
    public async Task UpdateAsync(Client client)
    {
        var existingClient = await context.Clients.FindAsync(client.Id) ??
            throw new KeyNotFoundException($"Client with Id {client.Id} not found.");

        existingClient.FullName = client.FullName;
        existingClient.PassportNumber = client.PassportNumber;
        existingClient.ContactPhone = client.ContactPhone;

        context.Clients.Update(existingClient);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a client by ID.
    /// </summary>
    /// <param name="id">Client ID.</param>
    public async Task DeleteAsync(int id)
    {
        var client = await context.Clients.FindAsync(id) ??
            throw new KeyNotFoundException($"Client with Id {id} not found.");

        context.Clients.Remove(client);
        await context.SaveChangesAsync();
    }
}
