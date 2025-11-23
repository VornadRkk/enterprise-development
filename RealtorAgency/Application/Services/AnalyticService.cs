using Application.Dtos.AnalyticsDtos;
using AutoMapper;
using Domain.Interfaces;
using Domain.Enums;

namespace Application.Services;

/// <summary>
/// Provides analytics operations related to clients, properties and requests.
/// </summary>
/// <param name="requestRepository">Repository for accessing requests.</param>
/// <param name="clientRepository">Repository for accessing clients.</param>
/// <param name="propertyRepository">Repository for accessing properties.</param>
/// <param name="mapper">Mapper for dtos.</param>
public class AnalyticsService(
    IRequestRepository requestRepository,
    IClientRepository clientRepository,
    IPropertyRepository propertyRepository,
    IMapper mapper
)
{
    /// <summary>
    /// Returns all sellers (clients with sale requests) within a specified date range, sorted by name.
    /// </summary>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    public async Task<List<ClientDto>> GetSellersInPeriodAsync(DateTime start, DateTime end)
    {
        var requests = await requestRepository.GetAllAsync();

        var sellers = requests
            .Where(r => r.Type == RequestType.Sale && r.Date >= start && r.Date <= end)
            .Select(r => r.Client)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        return mapper.Map<List<ClientDto>>(sellers);
    }

    /// <summary>
    /// Returns the top five clients by request count for a specific request type.
    /// </summary>
    /// <param name="type">The type of request (Sale or Purchase).</param>
    public async Task<List<ClientWithRequestCountDto>> GetTopClientsByRequestCountAsync(RequestType type)
    {
        var requests = await requestRepository.GetAllAsync();

        var topClients = requests
            .Where(r => r.Type == type)
            .GroupBy(r => r.Client)
            .Select(g =>
            {
                var dto = mapper.Map<ClientWithRequestCountDto>(g.Key);
                dto.Count = g.Count();
                return dto;
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.FullName)
            .Take(5)
            .ToList();

        return topClients;
    }

    /// <summary>
    /// Returns request counts grouped by property type.
    /// </summary>
    public async Task<Dictionary<string, int>> GetRequestCountByPropertyTypeAsync()
    {
        var requests = await requestRepository.GetAllAsync();

        var counts = requests
            .GroupBy(r => r.Property.Type)
            .ToDictionary(g => g.Key.ToString(), g => g.Count());

        return counts;
    }

    /// <summary>
    /// Returns clients who made requests with the minimum amount.
    /// </summary>
    public async Task<List<ClientWithAmountDto>> GetClientsWithMinAmountRequestAsync()
    {
        var requests = await requestRepository.GetAllAsync();

        if (!requests.Any())
            return [];

        var minAmount = requests.Min(r => r.Amount);

        var clients = requests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.Client)
            .DistinctBy(c => c.Id)
            .Select(c =>
            {
                var dto = mapper.Map<ClientWithAmountDto>(c);
                dto.Amount = minAmount;
                return dto;
            })
            .OrderBy(c => c.FullName)
            .ToList();

        return clients;
    }

    /// <summary>
    /// Returns clients searching for a specific property type (purchase requests), sorted by name.
    /// </summary>
    /// <param name="propertyType">The type of property being searched for.</param>
    public async Task<List<ClientDto>> GetClientsSearchingForPropertyTypeAsync(PropertyType propertyType)
    {
        var requests = await requestRepository.GetAllAsync();

        var clients = requests
            .Where(r => r.Type == RequestType.Purchase && r.Property.Type == propertyType)
            .Select(r => r.Client)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        return mapper.Map<List<ClientDto>>(clients);
    }

    /// <summary>
    /// Returns the top five property types by request count within a specified period.
    /// </summary>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    public async Task<List<PropertyTypeDto>> GetTopPropertyTypesByPeriodAsync(DateTime start, DateTime end)
    {
        var requests = await requestRepository.GetAllAsync();

        var topPropertyTypes = requests
            .Where(r => r.Date >= start && r.Date <= end)
            .GroupBy(r => r.Property.Type)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new PropertyTypeDto
            {
                PropertyType = g.Key.ToString(),
                Count = g.Count()
            })
            .ToList();

        return topPropertyTypes;
    }

    /// <summary>
    /// Returns clients with the highest total request amount.
    /// </summary>
    public async Task<List<ClientWithAmountDto>> GetClientsWithMaxTotalAmountAsync()
    {
        var requests = await requestRepository.GetAllAsync();

        var clientsWithTotalAmount = requests
            .GroupBy(r => r.Client)
            .Select(g =>
            {
                var dto = mapper.Map<ClientWithAmountDto>(g.Key);
                dto.Amount = g.Sum(r => r.Amount);
                return dto;
            })
            .ToList();

        if (!clientsWithTotalAmount.Any())
            return [];

        var maxAmount = clientsWithTotalAmount.Max(x => x.Amount);

        var topClients = clientsWithTotalAmount
            .Where(x => x.Amount == maxAmount)
            .OrderBy(c => c.FullName)
            .ToList();

        return topClients;
    }
}
