using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealtorAgency.Application;
using RealtorAgency.Application.Dtos.RepositoryDtos;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;

namespace RealtorAgency.Api.Controllers;

/// <summary>
/// Endpoints for managing properties.
/// </summary>
[ApiController]
[Route("api/properties")]
public class PropertyController(
    IRepository<Property> propertyRepository,
    ILogger<PropertyController> logger,
    IMapper mapper,
    IEnumValidationService enumValidationService
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
        if (property == null)
            return NotFound();

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
        try
        {
            await propertyRepository.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("Property with id {Id} not found for deletion", id);
        }

        return NoContent();
    }

    /// <summary>
    /// Creates a new property.
    /// </summary>
    /// <param name="newPropertyDto">The data of the property to create.</param>
    [HttpPost]
    public async Task<ActionResult<PropertyGetDto>> CreateProperty([FromBody] PropertyEditDto newPropertyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!enumValidationService.ValidatePropertyType(newPropertyDto.Type, out var propertyError))
            return BadRequest(propertyError);

        if (!enumValidationService.ValidatePurpose(newPropertyDto.Purpose, out var purposeError))
            return BadRequest(purposeError);

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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!enumValidationService.ValidatePropertyType(updatedPropertyDto.Type, out var propertyError))
            return BadRequest(propertyError);

        if (!enumValidationService.ValidatePurpose(updatedPropertyDto.Purpose, out var purposeError))
            return BadRequest(purposeError);

        var property = await propertyRepository.GetByIdAsync(id);
        if (property == null)
            return NotFound();

        var updatedProperty = mapper.Map<Property>(updatedPropertyDto);
        updatedProperty.Id = property.Id;
        await propertyRepository.UpdateAsync(updatedProperty);

        return NoContent();
    }
}
