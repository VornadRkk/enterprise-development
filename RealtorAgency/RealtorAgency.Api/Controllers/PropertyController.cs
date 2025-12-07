using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealtorAgency.Application;
using RealtorAgency.Application.Dtos.RepositoryDtos;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Domain.Interfaces;

namespace RealtorAgency.Api.Controllers;

/// <summary>
/// Endpoints for managing properties.
/// </summary>
/// <param name="propertyRepository">Repository for accessing property data.</param>
/// <param name="mapper">Mapper for dtos and entities.</param>
[ApiController]
[Route("api/properties")]
public class PropertyController(
    IRepository<Property> propertyRepository,
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
