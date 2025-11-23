using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Provides methods for managing <see cref="Client"/> entities in the repository.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Returns all clients.
    /// </summary>
    public Task<IEnumerable<Client>> GetAllAsync();

    /// <summary>
    /// Returns a client by its unique identifier.
    /// Returns null if the client does not exist.
    /// </summary>
    /// <param name="id">Client ID.</param>
    public Task<Client?> GetByIdAsync(int id);

    /// <summary>
    /// Checks if a client exists by its unique identifier.
    /// </summary>
    /// <param name="id">Client ID.</param>
    public Task<bool> ExistsById(int id);

    /// <summary>
    /// Adds a new client to the repository.
    /// </summary>
    /// <param name="client">Client to add.</param>
    public Task AddAsync(Client client);

    /// <summary>
    /// Updates an existing client in the repository.
    /// </summary>
    /// <param name="client">Client to update.</param>
    public Task UpdateAsync(Client client);

    /// <summary>
    /// Deletes a client by its unique identifier.
    /// </summary>
    /// <param name="id">Client ID.</param>
    public Task DeleteAsync(int id);
}

/// <summary>
/// Provides methods for managing <see cref="Property"/> entities in the repository.
/// </summary>
public interface IPropertyRepository
{
    /// <summary>
    /// Returns all properties.
    /// </summary>
    public Task<IEnumerable<Property>> GetAllAsync();

    /// <summary>
    /// Returns a property by its unique identifier.
    /// Returns null if the property does not exist.
    /// </summary>
    /// <param name="id">Property ID.</param>
    public Task<Property?> GetByIdAsync(int id);

    /// <summary>
    /// Checks if a property exists by its unique identifier.
    /// </summary>
    /// <param name="id">Property ID.</param>
    public Task<bool> ExistsById(int id);

    /// <summary>
    /// Adds a new property to the repository.
    /// </summary>
    /// <param name="property">Property to add.</param>
    public Task AddAsync(Property property);

    /// <summary>
    /// Updates an existing property in the repository.
    /// </summary>
    /// <param name="property">Property to update.</param>
    public Task UpdateAsync(Property property);

    /// <summary>
    /// Deletes a property by its unique identifier.
    /// </summary>
    /// <param name="id">Property ID.</param>
    public Task DeleteAsync(int id);
}

/// <summary>
/// Provides methods for managing <see cref="Request"/> entities in the repository.
/// </summary>
public interface IRequestRepository
{
    /// <summary>
    /// Returns all requests.
    /// </summary>
    public Task<IEnumerable<Request>> GetAllAsync();

    /// <summary>
    /// Returns a request by its unique identifier.
    /// Returns null if the request does not exist.
    /// </summary>
    /// <param name="id">Request ID.</param>
    public Task<Request?> GetByIdAsync(int id);

    /// <summary>
    /// Checks if a request exists by its unique identifier.
    /// </summary>
    /// <param name="id">Request ID.</param>
    public Task<bool> ExistsById(int id);

    /// <summary>
    /// Adds a new request to the repository.
    /// </summary>
    /// <param name="request">Request to add.</param>
    public Task AddAsync(Request request);

    /// <summary>
    /// Updates an existing request in the repository.
    /// </summary>
    /// <param name="request">Request to update.</param>
    public Task UpdateAsync(Request request);

    /// <summary>
    /// Deletes a request by its unique identifier.
    /// </summary>
    /// <param name="id">Request ID.</param>
    public Task DeleteAsync(int id);
}
