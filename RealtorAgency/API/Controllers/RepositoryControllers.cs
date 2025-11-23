using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.RepositoryDtos;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;

namespace Api.Controllers;

/// <summary>
/// Endpoints for managing clients.
/// </summary>
/// <param name="clientRepository">Repository for accessing client data.</param>
/// <param name="mapper">Mapper for dtos and entities.</param>
[ApiController]
[Route("api/clients")]
public class ClientController(
    IClientRepository clientRepository,
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
        var isExists = await clientRepository.ExistsById(id);
        if (!isExists) return NotFound();

        await clientRepository.DeleteAsync(id);
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

/// <summary>
/// Endpoints for managing properties.
/// </summary>
/// <param name="propertyRepository">Repository for accessing property data.</param>
/// <param name="mapper">Mapper for dtos and entities.</param>
[ApiController]
[Route("api/properties")]
public class PropertyController(
    IPropertyRepository propertyRepository,
    IMapper mapper
) : ControllerBase
{
    /// <summary>
    /// Returns all properties in the system.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PropertyGetDto>>> GetAllProperties()
    {
        var properties = await propertyRepository.GetAllAsync();
        var propertiesDto = mapper.Map<List<PropertyGetDto>>(properties);
        return Ok(propertiesDto);
    }

    /// <summary>
    /// Returns a property by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the property to return.</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PropertyGetDto>> GetPropertyById(int id)
    {
        var property = await propertyRepository.GetByIdAsync(id);
        if (property == null) return NotFound();

        var propertyDto = mapper.Map<PropertyGetDto>(property);
        return Ok(propertyDto);
    }

    /// <summary>
    /// Deletes a property by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the property to delete.</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePropertyById(int id)
    {
        var isExists = await propertyRepository.ExistsById(id);
        if (!isExists) return NotFound();

        await propertyRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new property.
    /// </summary>
    /// <param name="newPropertyDto">The data of the property to create.</param>
    [HttpPost]
    public async Task<ActionResult<PropertyGetDto>> CreateProperty([FromBody] PropertyEditDto newPropertyDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!Enum.IsDefined(typeof(PropertyType), newPropertyDto.Type))
            return BadRequest($"Invalid property type: {newPropertyDto.Type}");
        if (!Enum.IsDefined(typeof(Purpose), newPropertyDto.Purpose))
            return BadRequest($"Invalid purpose: {newPropertyDto.Purpose}");

        var newProperty = mapper.Map<Property>(newPropertyDto);
        await propertyRepository.AddAsync(newProperty);

        var resultDto = mapper.Map<PropertyGetDto>(newProperty);
        return CreatedAtAction(nameof(GetPropertyById), new { id = newProperty.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing property by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the property to update.</param>
    /// <param name="updatedPropertyDto">The updated property data.</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProperty(int id, [FromBody] PropertyEditDto updatedPropertyDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!Enum.IsDefined(typeof(PropertyType), updatedPropertyDto.Type))
            return BadRequest($"Invalid property type: {updatedPropertyDto.Type}");
        if (!Enum.IsDefined(typeof(Purpose), updatedPropertyDto.Purpose))
            return BadRequest($"Invalid purpose: {updatedPropertyDto.Purpose}");

        var property = await propertyRepository.GetByIdAsync(id);
        if (property == null) return NotFound();

        var updatedProperty = mapper.Map<Property>(updatedPropertyDto);
        updatedProperty.Id = property.Id;
        await propertyRepository.UpdateAsync(updatedProperty);
        return NoContent();
    }
}

/// <summary>
/// Endpoints for managing requests.
/// </summary>
/// <param name="requestRepository">Repository for accessing requests.</param>
/// <param name="clientRepository">Repository for accessing clients.</param>
/// <param name="propertyRepository">Repository for accessing properties.</param>
/// <param name="mapper">Mapper for dtos and entities.</param>
[ApiController]
[Route("api/requests")]
public class RequestController(
    IRequestRepository requestRepository,
    IClientRepository clientRepository,
    IPropertyRepository propertyRepository,
    IMapper mapper
) : ControllerBase
{
    /// <summary>
    /// Returns all requests.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<RequestGetDto>>> GetAllRequests()
    {
        var requests = await requestRepository.GetAllAsync();
        var requestsDto = mapper.Map<List<RequestGetDto>>(requests);
        return Ok(requestsDto);
    }

    /// <summary>
    /// Returns a request by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the request to return.</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RequestGetDto>> GetRequestById(int id)
    {
        var request = await requestRepository.GetByIdAsync(id);
        if (request == null)
            return NotFound();

        var requestDto = mapper.Map<RequestGetDto>(request);
        return Ok(requestDto);
    }

    /// <summary>
    /// Deletes a request by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the request to delete.</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRequestById(int id)
    {
        var isExists = await requestRepository.ExistsById(id);
        if (!isExists) return NotFound();

        await requestRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new request.
    /// </summary>
    /// <param name="newRequestDto">The data for the new request.</param>
    [HttpPost]
    public async Task<ActionResult<RequestGetDto>> CreateRequest([FromBody] RequestEditDto newRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isClientExists = await clientRepository.ExistsById(newRequestDto.ClientId);
        var isPropertyExists = await propertyRepository.ExistsById(newRequestDto.PropertyId);

        if (!isClientExists || !isPropertyExists) return NotFound("Client or Property not found");

        if (!Enum.IsDefined(typeof(RequestType), newRequestDto.Type))
            return BadRequest($"Invalid request type: {newRequestDto.Type}");

        var newRequest = mapper.Map<Request>(newRequestDto);
        await requestRepository.AddAsync(newRequest);

        var resultDto = mapper.Map<RequestGetDto>(newRequest);
        return CreatedAtAction(nameof(GetRequestById), new { id = newRequest.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing request by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the request to update.</param>
    /// <param name="updatedRequestDto">The updated request data.</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateRequest(int id, [FromBody] RequestEditDto updatedRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isClientExists = await clientRepository.ExistsById(updatedRequestDto.ClientId);
        var isPropertyExists = await propertyRepository.ExistsById(updatedRequestDto.PropertyId);

        if (!isClientExists || !isPropertyExists) return NotFound("Client or Property not found");

        if (!Enum.IsDefined(typeof(RequestType), updatedRequestDto.Type))
            return BadRequest($"Invalid request type: {updatedRequestDto.Type}");

        var request = await requestRepository.GetByIdAsync(id);
        if (request == null) return NotFound();

        var updatedRequest = mapper.Map<Request>(updatedRequestDto);
        updatedRequest.Id = request.Id;
        await requestRepository.UpdateAsync(updatedRequest);
        return NoContent();
    }
}
