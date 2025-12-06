using RealtorAgency.Producer;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<RabbitMqProducer>();

var host = builder.Build();
host.Run();
