using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RealtorAgency.Api.Consumer;
using RealtorAgency.Application;
using RealtorAgency.Application.Mappers;
using RealtorAgency.Application.Services;
using RealtorAgency.Domain.Entities;
using RealtorAgency.Domain.Interfaces;
using RealtorAgency.Infrastructure.Persistence;
using RealtorAgency.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))
    ));

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<AppMappingProfile>();
});

builder.Services.AddSingleton<IConnectionFactory>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("RabbitMQ connection string is not configured");

    return new ConnectionFactory
    {
        Uri = new Uri(connectionString)
    };
});

builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Property>, PropertyRepository>();
builder.Services.AddScoped<IRepository<Request>, RequestRepository>();


builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "RealtorAgency API",
        Description = "API for real estate agency analytics and management"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DbSeeder.SeedClientsAsync(db);
    await DbSeeder.SeedPropertiesAsync(db);
    await DbSeeder.SeedRequestsAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
