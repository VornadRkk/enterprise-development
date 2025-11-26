using Microsoft.EntityFrameworkCore;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;
using RealtorAgency.Infrastructure.Persistence;

namespace RealtorAgency.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Property entities.
/// Provides CRUD methods for properties.
/// </summary>
/// <param name="context">The application's database context used for data access.</param>
public class PropertyRepository(AppDbContext context) : IRepository<Property>
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
