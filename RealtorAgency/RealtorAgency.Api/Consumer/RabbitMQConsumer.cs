using AutoMapper;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;
using RealtorAgency.Application.Dtos.RepositoryDtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RealtorAgency.Api.Consumer;

/// <summary>
/// RabbitMQ consumer service that listens to messages for requests.
/// Implements <see cref="BackgroundService"/> to run continuously in the background.
/// </summary>
public class RabbitMqConsumer(
    IConnectionFactory connectionFactory,
    ILogger<RabbitMqConsumer> logger,
    IMapper mapper,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private const string ExchangeName = "realtor-agency-exchange";
    private const string QueueName = "request-queue";
    private const string RequestRoutingKey = "request.create";

    private async Task<IConnection> ConnectWithRetryAsync(
        CancellationToken stoppingToken,
        int maxRetries = 5,
        int delayMs = 1000)
    {
        var attempt = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                attempt++;
                var connection = await connectionFactory.CreateConnectionAsync(stoppingToken);
                logger.LogInformation("RabbitMQ connection established on try {Attempt}", attempt);
                return connection;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Connection to RabbitMQ failed (try {Attempt})", attempt);
                if (attempt >= maxRetries)
                {
                    logger.LogError("Reached maximum connection attempts ({MaxRetries}). Unable to proceed.", maxRetries);
                    throw;
                }
                await Task.Delay(delayMs, stoppingToken);
            }
        }
        throw new OperationCanceledException("Connection attempt was cancelled.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await using var connection = await ConnectWithRetryAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: stoppingToken);

            await channel.QueueBindAsync(
                queue: QueueName,
                exchange: ExchangeName,
                routingKey: RequestRoutingKey,
                cancellationToken: stoppingToken);

            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    logger.LogInformation("Message arrived from {RoutingKey}: {Json}", ea.RoutingKey, json);

                    await ProcessRequestAsync(json);

                    await channel.BasicAckAsync(
                        ea.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Message processing failed");
                    await channel.BasicNackAsync(
                        ea.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        cancellationToken: stoppingToken);
                }
            };

            await channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            logger.LogCritical(ex, "RabbitMQ consumer startup failed");
            throw;
        }
    }

    private async Task ProcessRequestAsync(string json)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var requestRepository = scope.ServiceProvider.GetRequiredService<IRepository<Request>>();
            var clientRepository = scope.ServiceProvider.GetRequiredService<IRepository<Client>>();
            var propertyRepository = scope.ServiceProvider.GetRequiredService<IRepository<Property>>();
            var requestDto = JsonSerializer.Deserialize<RequestEditDto>(json);
            if (requestDto == null)
            {
                logger.LogWarning("Malformed request detected in message: {Json}", json);
                return;
            }

            var client = await clientRepository.GetByIdAsync(requestDto.ClientId);
            if (client == null)
            {
                logger.LogWarning("Referenced client (ID {ClientId}) does not exist. Request discarded.", requestDto.ClientId);
                return;
            }

            var property = await propertyRepository.GetByIdAsync(requestDto.PropertyId);
            if (property == null)
            {
                logger.LogWarning("Referenced property (ID {PropertyId}) does not exist. Request discarded.", requestDto.PropertyId);
                return;
            }

            var request = mapper.Map<Request>(requestDto);

            request.Client = client;
            request.Property = property;

            await requestRepository.AddAsync(request);

            logger.LogInformation("Request persisted: ClientId={ClientId}, PropertyId={PropertyId}, RequestType={Type}",
                requestDto.ClientId, requestDto.PropertyId, requestDto.Type);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process incoming request. Message content: {Json}", json);
            throw;
        }
    }
}
