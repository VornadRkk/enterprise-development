using RealtorAgency.Application.Dtos.RepositoryDtos;
using AutoMapper;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RealtorAgency.Api.Controllers;

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
    IRepository<Request> requestRepository,
    IRepository<Client> clientRepository,
    IRepository<Property> propertyRepository,
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
