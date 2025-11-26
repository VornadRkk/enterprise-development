using Microsoft.EntityFrameworkCore;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;
using RealtorAgency.Infrastructure.Persistence;

namespace RealtorAgency.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Request entities.
/// Provides CRUD methods for requests.
/// </summary>
/// <param name="context">The application's database context used for data access.</param>
public class RequestRepository(AppDbContext context) : IRepository<Request>
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
