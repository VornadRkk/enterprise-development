using RealtorAgency.Application.Dtos.AnalyticsDtos;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Enums;
using RealtorAgency.Domain.Interfaces;
using AutoMapper;

namespace RealtorAgency.Application.Services;

/// <summary>
/// Provides analytics operations related to clients, properties and requests.
/// </summary>
public class AnalyticsService(
    IRepository<Request> requestRepository,
    IMapper mapper
) : IAnalyticsService
{
    /// <summary>
    /// Returns all sellers (clients with sale requests) within a specified date range, sorted by name.
    /// </summary>
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
    public async Task<List<ClientWithRequestCountDto>> GetTopClientsByRequestCountAsync(RequestType type)
    {
        var requests = await requestRepository.GetAllAsync();

        var topClients = requests
            .Where(r => r.Type == type)
            .GroupBy(r => r.Client)
            .Select(g => new ClientWithRequestCountDto
            {
                Id = g.Key.Id,
                FullName = g.Key.FullName,
                PassportNumber = g.Key.PassportNumber,
                ContactPhone = g.Key.ContactPhone,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
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
            .ToDictionary(
                g => g.Key.ToString(),
                g => g.Count()
            );

        return counts;
    }

    /// <summary>
    /// Returns clients who made requests with the minimum amount.
    /// </summary>
    public async Task<List<ClientWithAmountDto>> GetClientsWithMinAmountRequestAsync()
    {
        var requests = await requestRepository.GetAllAsync();

        var minAmount = requests.Min(r => r.Amount);

        var clients = requests
            .Where(r => r.Amount == minAmount)
            .Select(r => new ClientWithAmountDto
            {
                Id = r.Client.Id,
                FullName = r.Client.FullName,
                PassportNumber = r.Client.PassportNumber,
                ContactPhone = r.Client.ContactPhone,
                Amount = r.Amount
            })
            .DistinctBy(c => c.Id)
            .ToList();

        return clients;
    }

    /// <summary>
    /// Returns clients searching for a specific property type (purchase requests), sorted by name.
    /// </summary>
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
    public async Task<List<PropertyTypeDto>> GetTopPropertyTypesByPeriodAsync(DateTime start, DateTime end)
    {
        var requests = await requestRepository.GetAllAsync();

        var topPropertyTypes = requests
            .Where(r => r.Date >= start && r.Date <= end)
            .GroupBy(r => r.Property.Type)
            .Select(g => new PropertyTypeDto
            {
                PropertyType = g.Key.ToString(),
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        return topPropertyTypes;
    }

    /// <summary>
    /// Returns clients with the highest total request amount.
    /// </summary>
    public async Task<List<ClientWithAmountDto>> GetClientsWithMaxTotalAmountAsync()
    {
        var requests = await requestRepository.GetAllAsync();

        var clientsWithMaxAmount = requests
            .GroupBy(r => r.Client)
            .Select(g => new ClientWithAmountDto
            {
                Id = g.Key.Id,
                FullName = g.Key.FullName,
                PassportNumber = g.Key.PassportNumber,
                ContactPhone = g.Key.ContactPhone,
                Amount = g.Sum(r => r.Amount)
            })
            .OrderByDescending(x => x.Amount)
            .Take(5)
            .ToList();

        return clientsWithMaxAmount;
    }
}
