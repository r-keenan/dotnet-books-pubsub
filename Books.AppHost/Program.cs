var builder = DistributedApplication.CreateBuilder(args);

builder.AddPostgres("postgres").WithPgAdmin().AddDatabase("books");
builder.AddRabbitMQ("rabbitmq");
var kafka = builder.AddContainer("kafka", "confluentinc/cp-kafka:latest")
    .WithEnvironment("KAFKA_ADVERTISED_LISTENERS", "PLAINTEXT://kafka:9092")
    .WithEnvironment("KAFKA_LISTENER_SECURITY_PROTOCOL_MAP", "PLAINTEXT:PLAINTEXT,CONTROLLER:PLAINTEXT")
    .WithEnvironment("KAFKA_INTER_BROKER_LISTENER_NAME", "PLAINTEXT")
    .WithEnvironment("KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_NODE_ID", "1")
    .WithEnvironment("KAFKA_CONTROLLER_QUORUM_VOTERS", "1@kafka:9093")
    .WithEnvironment("KAFKA_PROCESS_ROLES", "broker,controller")
    .WithEnvironment("KAFKA_CONTROLLER_LISTENER_NAMES", "CONTROLLER")
    .WithEnvironment("KAFKA_LISTENERS", "PLAINTEXT://kafka:9092,CONTROLLER://kafka:9093")
    .WithEndpoint(9092, 9092)
    .WithEndpoint(9093, 9093);

var controlCenter = builder.AddContainer("control-center", "confluentinc/cp-enterprise-control-center:latest")
    .WithEnvironment("CONTROL_CENTER_BOOTSTRAP_SERVERS", "kafka:9092")
    .WithEnvironment("CONTROL_CENTER_REPLICATION_FACTOR", "1")
    .WithEnvironment("PORT", "9021")
    .WithEndpoint(9021, 9021, "Control Center");  // Expose UI port

builder.AddProject<Projects.Books_API>("books-api").WithReference(kafka.GetEndpoint("kafka"));

builder.Build().Run();
