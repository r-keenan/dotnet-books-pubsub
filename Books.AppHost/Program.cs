using Aspire.Hosting;
using Books.AppHost.Extensions;
using Books.Shared.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin().AddDatabase("books");
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var kafka = builder
    // AddContainer automatically uses the latest tag. you can specify a specific tag as another parameter if you want to
    .AddContainer("kafka", "docker.io/confluentinc/cp-kafka")
    .WithEnvironment("CLUSTER_ID", "MkU3OEVBNTcwNTJENDM2Qk")
    .WithEnvironment("KAFKA_NODE_ID", "1")
    .WithEnvironment("KAFKA_PROCESS_ROLES", "broker,controller")
    .WithEnvironment("KAFKA_CONTROLLER_QUORUM_VOTERS", "1@0.0.0.0:9093")
    .WithEnvironment(
        "KAFKA_LISTENERS",
        "BROKER://0.0.0.0:29092,CONTROLLER://0.0.0.0:9093,EXTERNAL://0.0.0.0:9092"
    )
    .WithEnvironment(
        "KAFKA_ADVERTISED_LISTENERS",
        "BROKER://localhost:29092,EXTERNAL://localhost:9092"
    )
    .WithEnvironment(
        "KAFKA_LISTENER_SECURITY_PROTOCOL_MAP",
        "CONTROLLER:PLAINTEXT,BROKER:PLAINTEXT,EXTERNAL:PLAINTEXT"
    )
    .WithEnvironment("KAFKA_CONTROLLER_LISTENER_NAMES", "CONTROLLER")
    .WithEnvironment("KAFKA_INTER_BROKER_LISTENER_NAME", "BROKER")
    .WithEnvironment("KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS", "0")
    .WithEnvironment("KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_TRANSACTION_STATE_LOG_MIN_ISR", "1")
    .WithEnvironment("KAFKA_LOG_DIRS", "/var/lib/kafka/data")
    .WithEndpoint(9092, 9092, name: "external")
    .WithEndpoint(9093, 9093, name: "controller")
    .WithEndpoint(29092, 29092, name: "broker")
    .WithTopics(KafkaTopics.GetAllTopics(), numPartitions: 3, replicationFactor: 1)
    .WithVolume("kafka_data", "/var/lib/kafka/data")
    .WithVolume("kafka_metadata", "/var/lib/kafka/metadata");

var controlCenter = builder
    .AddContainer("control-center", "docker.io/confluentinc/cp-enterprise-control-center")
    .WithEnvironment("CONTROL_CENTER_BOOTSTRAP_SERVERS", "PLAINTEXT://0.0.0.0:9092")
    .WithEnvironment("CONTROL_CENTER_REPLICATION_FACTOR", "1")
    .WithEnvironment("CONTROL_CENTER_INTERNAL_TOPICS_PARTITIONS", "1")
    .WithEnvironment("CONTROL_CENTER_MONITORING_INTERCEPTOR_TOPIC_PARTITIONS", "1")
    .WithEnvironment("CONFLUENT_METRICS_TOPIC_REPLICATION", "1")
    .WithEnvironment("CONFLUENT_CONTROLCENTER_INTERNAL_TOPICS_REPLICATION", "1")
    .WithEnvironment("PORT", "9021")
    .WithEndpoint(9021, 9021, name: "control-center")
    .WithReference(kafka.GetEndpoint("external")); // Expose UI port

builder
    .AddProject<Projects.Books_API>("books-api")
    .WithReference(kafka.GetEndpoint("broker"))
    .WithReference(postgres)
    .WithReference(rabbitmq);

builder.Build().Run();
