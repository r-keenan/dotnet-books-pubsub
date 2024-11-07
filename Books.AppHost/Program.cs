var builder = DistributedApplication.CreateBuilder(args);

builder.AddPostgres("postgres").WithPgAdmin().AddDatabase("books");
builder.AddRabbitMQ("rabbitmq");

builder.AddProject<Projects.Books_API>("books-api");

builder.Build().Run();
