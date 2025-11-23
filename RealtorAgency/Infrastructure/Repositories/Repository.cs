using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository for managing Client entities.
/// Provides CRUD methods for clients.
/// </summary>
/// <param name="context">The application's database context used for data access.</param>
public class ClientRepository(AppDbContext context) : IClientRepository
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

/// <summary>
/// Repository for managing Property entities.
/// Provides CRUD methods for properties.
/// </summary>
/// <param name="context">The application's database context used for data access.</param>
public class PropertyRepository(AppDbContext context) : IPropertyRepository
{
    /// <summary>
    /// Gets all properties.
    /// </summary>
    public async Task<IEnumerable<Property>> GetAllAsync() =>
        await context.Properties.ToListAsync();

    /// <summary>
    /// Gets a property by its ID.
    /// </summary>
    /// <param name="id">Property ID.</param>
    /// <returns>The <see cref="Property"/> if found; otherwise, null.</returns>
    public async Task<Property?> GetByIdAsync(int id) =>
        await context.Properties.FirstOrDefaultAsync(p => p.Id == id);

    /// <summary>
    /// Checks if a property exists by ID.
    /// </summary>
    /// <param name="id">Property ID.</param>
    /// <returns>True if exists; otherwise, false.</returns>
    public async Task<bool> ExistsById(int id) =>
        await context.Properties.AnyAsync(p => p.Id == id);

    /// <summary>
    /// Adds a new property.
    /// </summary>
    /// <param name="property">Property to add.</param>
    public async Task AddAsync(Property property)
    {
        await context.Properties.AddAsync(property);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing property.
    /// </summary>
    /// <param name="property">Property with updated data.</param>
    public async Task UpdateAsync(Property property)
    {
        var existingProperty = await context.Properties.FindAsync(property.Id) ??
            throw new KeyNotFoundException($"Property with Id {property.Id} not found.");

        existingProperty.Type = property.Type;
        existingProperty.Purpose = property.Purpose;
        existingProperty.CadastralNumber = property.CadastralNumber;
        existingProperty.Address = property.Address;
        existingProperty.Floors = property.Floors;
        existingProperty.TotalArea = property.TotalArea;
        existingProperty.Rooms = property.Rooms;
        existingProperty.CeilingHeight = property.CeilingHeight;
        existingProperty.FloorNumber = property.FloorNumber;
        existingProperty.HasEncumbrances = property.HasEncumbrances;

        context.Properties.Update(existingProperty);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a property by ID.
    /// </summary>
    /// <param name="id">Property ID.</param>
    public async Task DeleteAsync(int id)
    {
        var property = await context.Properties.FindAsync(id) ??
            throw new KeyNotFoundException($"Property with Id {id} not found.");

        context.Properties.Remove(property);
        await context.SaveChangesAsync();
    }
}

/// <summary>
/// Repository for managing Request entities.
/// Provides CRUD methods for requests.
/// </summary>
/// <param name="context">The application's database context used for data access.</param>
public class RequestRepository(AppDbContext context) : IRequestRepository
{
    /// <summary>
    /// Gets all requests.
    /// </summary>
    public async Task<IEnumerable<Request>> GetAllAsync() =>
        await context.Requests
            .Include(r => r.Client)
            .Include(r => r.Property)
            .ToListAsync();

    /// <summary>
    /// Gets a request by its ID.
    /// </summary>
    /// <param name="id">Request ID.</param>
    /// <returns>The <see cref="Request"/> if found; otherwise, null.</returns>
    public async Task<Request?> GetByIdAsync(int id) =>
        await context.Requests
            .Include(r => r.Client)
            .Include(r => r.Property)
            .FirstOrDefaultAsync(r => r.Id == id);

    /// <summary>
    /// Checks if a request exists by ID.
    /// </summary>
    /// <param name="id">Request ID.</param>
    /// <returns>True if exists; otherwise, false.</returns>
    public async Task<bool> ExistsById(int id) =>
        await context.Requests.AnyAsync(r => r.Id == id);

    /// <summary>
    /// Adds a new request.
    /// </summary>
    /// <param name="request">Request to add.</param>
    public async Task AddAsync(Request request)
    {
        await context.Requests.AddAsync(request);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing request.
    /// </summary>
    /// <param name="request">Request with updated data.</param>
    public async Task UpdateAsync(Request request)
    {
        var existingRequest = await context.Requests.FindAsync(request.Id) ??
            throw new KeyNotFoundException($"Request with Id {request.Id} not found.");

        existingRequest.Client = request.Client;
        existingRequest.Property = request.Property;
        existingRequest.Type = request.Type;
        existingRequest.Amount = request.Amount;
        existingRequest.Date = request.Date;

        context.Requests.Update(existingRequest);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a request by ID.
    /// </summary>
    /// <param name="id">Request ID.</param>
    public async Task DeleteAsync(int id)
    {
        var request = await context.Requests.FindAsync(id) ??
            throw new KeyNotFoundException($"Request with Id {id} not found.");

        context.Requests.Remove(request);
        await context.SaveChangesAsync();
    }
}
