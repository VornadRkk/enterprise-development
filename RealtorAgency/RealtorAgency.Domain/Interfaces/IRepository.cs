namespace RealtorAgency.Domain.Interfaces;

/// <summary>
/// Generic repository interface providing common CRUD operations for all entities.
/// </summary>
/// <typeparam name="T">The entity type managed by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Returns all entities.
    /// </summary>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Returns an entity by its unique identifier.
    /// Returns null if the entity does not exist.
    /// </summary>
    /// <param name="id">Entity ID.</param>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Checks if an entity exists by its unique identifier.
    /// </summary>
    /// <param name="id">Entity ID.</param>
    public Task<bool> ExistsById(int id);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    public Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">Entity ID.</param>
    public Task DeleteAsync(int id);
}
