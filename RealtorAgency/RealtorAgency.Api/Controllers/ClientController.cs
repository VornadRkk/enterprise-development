using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealtorAgency.Application.Dtos.RepositoryDtos;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;

namespace RealtorAgency.Api.Controllers;

/// <summary>
/// Endpoints for managing clients.
/// </summary>
/// <param name="clientRepository">Repository for accessing client data.</param>
/// <param name="logger">The logger instance.</param>
/// <param name="mapper">Mapper for dtos and entities.</param>
[ApiController]
[Route("api/clients")]
public class ClientController(
    IRepository<Client> clientRepository,
    ILogger<RequestController> logger,
    IMapper mapper
) : ControllerBase
{
    /// <summary>
    /// Returns all clients.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ClientGetDto>>> GetAllClients()
    {
        var clients = await clientRepository.GetAllAsync();
        var clientsDto = mapper.Map<List<ClientGetDto>>(clients);
        return Ok(clientsDto);
    }

    /// <summary>
    /// Returns a client by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the client to return.</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientGetDto>> GetClientById(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client == null) return NotFound();

        var clientDto = mapper.Map<ClientGetDto>(client);
        return Ok(clientDto);
    }

    /// <summary>
    /// Deletes a client by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the client to delete.</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteClientById(int id)
    {
        try
        {
            await clientRepository.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Client with id {Id} not found for deletion", id);
        }

        return NoContent();
    }


    /// <summary>
    /// Creates a new client.
    /// </summary>
    /// <param name="newClientDto">The data of the client to create.</param>
    [HttpPost]
    public async Task<ActionResult<ClientGetDto>> CreateClient([FromBody] ClientEditDto newClientDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var newClient = mapper.Map<Client>(newClientDto);
        await clientRepository.AddAsync(newClient);

        var resultDto = mapper.Map<ClientGetDto>(newClient);
        return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing client by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the client to update.</param>
    /// <param name="updatedClientDto">The updated client data.</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateClient(int id, [FromBody] ClientEditDto updatedClientDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var client = await clientRepository.GetByIdAsync(id);
        if (client == null) return NotFound();

        var updatedClient = mapper.Map<Client>(updatedClientDto);
        updatedClient.Id = client.Id;
        await clientRepository.UpdateAsync(updatedClient);
        return NoContent();
    }
}
