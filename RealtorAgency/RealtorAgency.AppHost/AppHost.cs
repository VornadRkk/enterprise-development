var builder = DistributedApplication.CreateBuilder(args);
var mysql = builder.AddMySql("mysql");
var mysqlDb = mysql.AddDatabase("realestate");

var username = builder.AddParameter("rabbitmq-username", secret: true);
var password = builder.AddParameter("rabbitmq-password", secret: true);

var rabbitMq = builder.AddRabbitMQ("RabbitMQ", username, password)
    .WithManagementPlugin();

builder.AddProject<Projects.RealtorAgency_Api>("RealtorAgencyApi")
    .WithReference(mysqlDb, "DefaultConnection") 
    .WithReference(rabbitMq)                      
    .WaitFor(mysqlDb)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.RealtorAgency_Producer>("RealtorAgencyProducer")
    .WithReference(rabbitMq)                      
    .WaitFor(rabbitMq)
    .WithEnvironment("RabbitMqPublishDelayMs", "100");

builder.Build().Run();