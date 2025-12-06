var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");

var mysqlDb = mysql.AddDatabase("realestate");

builder.AddProject<Projects.RealtorAgency_Api>("RealtorAgencyApi")
    .WithReference(mysqlDb, "DefaultConnection")
    .WaitFor(mysqlDb);

builder.AddProject<Projects.RealtorAgency_Producer>("realtoragency-producer");


builder.Build().Run();
