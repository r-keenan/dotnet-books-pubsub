using Aspire.Hosting;
using Books.AppHost.Extensions;
using Books.Shared.Constants;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddPostgres("postgres").WithPgAdmin().AddDatabase("books");
builder.AddRabbitMQ("rabbitmq");
var kafka = builder
        // AddContainer automatically uses the latest tag. you can specify a specific tag as another parameter if you want to
    .AddContainer("kafka", "docker.io/confluentinc/cp-kafka")
    .WithEnvironment("CLUSTER_ID", "MkU3OEVBNTcwNTJENDM2Qk")
    .WithEnvironment("KAFKA_NODE_ID", "1")
    .WithEnvironment("KAFKA_PROCESS_ROLES", "broker,controller")
    .WithEnvironment("KAFKA_CONTROLLER_QUORUM_VOTERS", "1@kafka:9093")
    .WithEnvironment("KAFKA_LISTENERS", "PLAINTEXT://0.0.0.0:9092,CONTROLLER://0.0.0.0:9093")
    .WithEnvironment("KAFKA_ADVERTISED_LISTENERS", "PLAINTEXT://kafka:9092")
    .WithEnvironment(
        "KAFKA_LISTENER_SECURITY_PROTOCOL_MAP",
        "PLAINTEXT:PLAINTEXT,CONTROLLER:PLAINTEXT"
    )
    .WithEnvironment("KAFKA_CONTROLLER_LISTENER_NAMES", "CONTROLLER")
    .WithEnvironment("KAFKA_INTER_BROKER_LISTENER_NAME", "PLAINTEXT")
    .WithEnvironment("KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_TRANSACTION_STATE_LOG_MIN_ISR", "1")
    .WithEndpoint(9092, 9092, name: "broker")
    .WithEndpoint(9093, 9093, name: "controller")
    .WithTopics(KafkaTopics.GetAllTopics(), numPartitions: 3, replicationFactor: 1);

var controlCenter = builder
    .AddContainer("control-center", "docker.io/confluentinc/cp-enterprise-control-center")
    .WithEnvironment("CONTROL_CENTER_BOOTSTRAP_SERVERS", "kafka:9092")
    .WithEnvironment("CONTROL_CENTER_REPLICATION_FACTOR", "1")
    .WithEnvironment("CONTROL_CENTER_INTERNAL_TOPICS_PARTITIONS", "1")
    .WithEnvironment("CONTROL_CENTER_MONITORING_INTERCEPTOR_TOPIC_PARTITIONS", "1")
    .WithEnvironment("CONFLUENT_METRICS_TOPIC_REPLICATION", "1")
    .WithEndpoint(9021, 9021, name: "control-center")
    .WithReference(kafka.GetEndpoint("broker")); // Expose UI port

builder.AddProject<Projects.Books_API>("books-api").WithReference(kafka.GetEndpoint("broker"));

builder.Build().Run();
