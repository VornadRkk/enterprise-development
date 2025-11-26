using Microsoft.AspNetCore.Mvc;
using RealtorAgency.Application;
using RealtorAgency.Application.Dtos.AnalyticsDtos;
using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Api.Controllers;

/// <summary>
/// Analytics endpoints for clients, properties, and requests.
/// </summary>
/// <param name="analyticsService">Service that provides analytics operations.</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    private readonly IAnalyticsService _analyticsService = analyticsService;

    /// <summary>
    /// Returns all sellers (clients with sale requests) within a specified date range.
    /// </summary>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    [HttpGet("sellers")]
    public async Task<ActionResult<List<ClientDto>>> GetSellersInPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var result = await _analyticsService.GetSellersInPeriodAsync(start, end);
        return Ok(result);
    }

    /// <summary>
    /// Returns the top five sellers based on request count.
    /// </summary>
    [HttpGet("top-sellers")]
    public async Task<ActionResult<List<ClientWithRequestCountDto>>> GetTopSellers()
    {
        var result = await _analyticsService.GetTopClientsByRequestCountAsync(RequestType.Sale);
        return Ok(result);
    }

    /// <summary>
    /// Returns the top five buyers based on request count.
    /// </summary>
    [HttpGet("top-buyers")]
    public async Task<ActionResult<List<ClientWithRequestCountDto>>> GetTopBuyers()
    {
        var result = await _analyticsService.GetTopClientsByRequestCountAsync(RequestType.Purchase);
        return Ok(result);
    }

    /// <summary>
    /// Returns request counts grouped by property type.
    /// </summary>
    [HttpGet("requests-by-property-type")]
    public async Task<ActionResult<Dictionary<string, int>>> GetRequestCountByPropertyType()
    {
        var result = await _analyticsService.GetRequestCountByPropertyTypeAsync();
        return Ok(result);
    }

    /// <summary>
    /// Returns clients who made requests with the minimum amount.
    /// </summary>
    [HttpGet("clients-min-amount")]
    public async Task<ActionResult<List<ClientWithAmountDto>>> GetClientsWithMinAmount()
    {
        var result = await _analyticsService.GetClientsWithMinAmountRequestAsync();
        return Ok(result);
    }

    /// <summary>
    /// Returns clients searching for a specific property type (purchase requests).
    /// </summary>
    /// <param name="propertyType">The type of property being searched for.</param>
    [HttpGet("clients-searching")]
    public async Task<ActionResult<List<ClientDto>>> GetClientsSearchingForPropertyType([FromQuery] PropertyType propertyType)
    {
        var result = await _analyticsService.GetClientsSearchingForPropertyTypeAsync(propertyType);
        return Ok(result);
    }

    /// <summary>
    /// Returns the top five property types by request count within the last year.
    /// </summary>
    [HttpGet("top-property-types")]
    public async Task<ActionResult<List<PropertyTypeDto>>> GetTopPropertyTypesLastYear()
    {
        var end = DateTime.Now;
        var start = end.AddYears(-1);
        var result = await _analyticsService.GetTopPropertyTypesByPeriodAsync(start, end);
        return Ok(result);
    }

    /// <summary>
    /// Returns clients with the highest total request amount.
    /// </summary>
    [HttpGet("clients-max-amount")]
    public async Task<ActionResult<List<ClientWithAmountDto>>> GetClientsWithMaxTotalAmount()
    {
        var result = await _analyticsService.GetClientsWithMaxTotalAmountAsync();
        return Ok(result);
    }
}
