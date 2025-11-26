using RealtorAgency.Application.Dtos.AnalyticsDtos;
using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Application;

/// <summary>
/// Provides analytics operations related to clients, properties and requests.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Returns all sellers (clients with sale requests) within a specified date range, sorted by name.
    /// </summary>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    public Task<List<ClientDto>> GetSellersInPeriodAsync(DateTime start, DateTime end);

    /// <summary>
    /// Returns the top five clients by request count for a specific request type.
    /// </summary>
    /// <param name="type">The type of request (Sale or Purchase).</param>
    public Task<List<ClientWithRequestCountDto>> GetTopClientsByRequestCountAsync(RequestType type);

    /// <summary>
    /// Returns request counts grouped by property type.
    /// </summary>
    public Task<Dictionary<string, int>> GetRequestCountByPropertyTypeAsync();

    /// <summary>
    /// Returns clients who made requests with the minimum amount.
    /// </summary>
    public Task<List<ClientWithAmountDto>> GetClientsWithMinAmountRequestAsync();

    /// <summary>
    /// Returns clients searching for a specific property type (purchase requests), sorted by name.
    /// </summary>
    /// <param name="propertyType">The type of property being searched for.</param>
    public Task<List<ClientDto>> GetClientsSearchingForPropertyTypeAsync(PropertyType propertyType);

    /// <summary>
    /// Returns the top five property types by request count within a specified period.
    /// </summary>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    public Task<List<PropertyTypeDto>> GetTopPropertyTypesByPeriodAsync(DateTime start, DateTime end);

    /// <summary>
    /// Returns clients with the highest total request amount.
    /// </summary>
    public Task<List<ClientWithAmountDto>> GetClientsWithMaxTotalAmountAsync();
}
