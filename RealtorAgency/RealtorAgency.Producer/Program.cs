using RealtorAgency.Producer;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddSingleton<IConnectionFactory>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("RabbitMQ connection string is not configured");

    return new ConnectionFactory
    {
        Uri = new Uri(connectionString)
    };
});

builder.Services.AddHostedService<RabbitMqProducer>();
var host = builder.Build();
host.Run();