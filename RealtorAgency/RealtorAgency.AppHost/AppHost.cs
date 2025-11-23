var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");

var mysqlDb = mysql.AddDatabase("realestate");

var api = builder.AddProject<Projects.API>("RealtorAgencyApi")
    .WithReference(mysqlDb, "DefaultConnection")
    .WaitFor(mysqlDb);

builder.Build().Run();
