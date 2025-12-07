using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealtorAgency.Application.Dtos.RepositoryDtos;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Domain.Interfaces;

namespace RealtorAgency.Api.Controllers;

/// <summary>
/// Endpoints for managing requests.
/// </summary>
[ApiController]
[Route("api/requests")]
public class RequestController(
    IRepository<Request> requestRepository,
    IRepository<Client> clientRepository,
    IRepository<Property> propertyRepository,
    ILogger<RequestController> logger,
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
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRequestById(int id)
    {
        try
        {
            await requestRepository.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Request with id {Id} not found for deletion", id);
        }

        return NoContent();
    }



    /// <summary>
    /// Creates a new request.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RequestGetDto>> CreateRequest([FromBody] RequestEditDto newRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = await clientRepository.GetByIdAsync(newRequestDto.ClientId);
        if (client == null)
            return NotFound("Client not found");

        var property = await propertyRepository.GetByIdAsync(newRequestDto.PropertyId);
        if (property == null)
            return NotFound("Property not found");

        if (!ValidateRequestType(newRequestDto.Type, out var errorMessage))
            return BadRequest(errorMessage);

        var newRequest = mapper.Map<Request>(newRequestDto);
        newRequest.Client = client;
        newRequest.Property = property;

        await requestRepository.AddAsync(newRequest);

        var resultDto = mapper.Map<RequestGetDto>(newRequest);
        return CreatedAtAction(nameof(GetRequestById), new { id = newRequest.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing request by its unique ID.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<RequestGetDto>> UpdateRequest(int id, [FromBody] RequestEditDto updatedRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = await clientRepository.GetByIdAsync(updatedRequestDto.ClientId);
        if (client == null)
            return NotFound("Client not found");

        var property = await propertyRepository.GetByIdAsync(updatedRequestDto.PropertyId);
        if (property == null)
            return NotFound("Property not found");

        if (!ValidateRequestType(updatedRequestDto.Type, out var errorMessage))
            return BadRequest(errorMessage);

        var request = await requestRepository.GetByIdAsync(id);
        if (request == null)
            return NotFound();

        var updatedRequest = mapper.Map<Request>(updatedRequestDto);
        updatedRequest.Id = request.Id;
        updatedRequest.Client = client;
        updatedRequest.Property = property;

        await requestRepository.UpdateAsync(updatedRequest);

        var resultDto = mapper.Map<RequestGetDto>(updatedRequest);
        return Ok(resultDto);
    }

    /// <summary>
    /// Validates the RequestType and returns error message with available options if invalid.
    /// </summary>
    private static bool ValidateRequestType(string type, out string errorMessage)
    {
        errorMessage = string.Empty;

        var validTypes = Enum.GetNames(typeof(RequestType));

        if (Enum.IsDefined(typeof(RequestType), type))
            return true;

        var validTypesString = string.Join(", ", validTypes);
        errorMessage = $"Invalid request type: '{type}'. Allowed values: {validTypesString}";

        return false;
    }
}
