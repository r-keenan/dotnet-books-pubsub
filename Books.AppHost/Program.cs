using Books.AppHost;
using Books.AppHost.Extensions;
using Books.Shared.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithEndpoint(5432, 5432, name: "postgresql")
    .WithVolume("postgres_data", "/var/lib/postgresql/data")
    .WithPgAdmin()
    .AddDatabase("books");

var rabbitmq = builder
    .AddRabbitMQ("rabbitmq")
    .WithManagementPlugin(port: 15762)
    .WithEndpoint(5762, 5672, name: "rabbitmq");

var kafka = builder
    // AddContainer automatically uses the latest tag. you can specify a specific tag as another parameter if you want to
    .AddContainer("kafka", "docker.io/confluentinc/cp-kafka")
    .WithEnvironment("CLUSTER_ID", "MkU3OEVBNTcwNTJENDM2Qk")
    .WithEnvironment("KAFKA_BROKER_ID", "1")
    .WithEnvironment("KAFKA_NODE_ID", "1")
    .WithEnvironment("KAFKA_HEAP_OPTS", "-Xmx512M -Xms512M")
    .WithEnvironment("KAFKA_MIN_INSYNC_REPLICAS", "1")
    .WithEnvironment("KAFKA_AUTO_CREATE_TOPICS_ENABLE", "true")
    .WithEnvironment("KAFKA_NUM_PARTITIONS", "1")
    .WithEnvironment("KAFKA_DEFAULT_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_PROCESS_ROLES", "controller,broker")
    .WithEnvironment("KAFKA_CONTROLLER_QUORUM_VOTERS", "1@0.0.0.0:9093")
    .WithEnvironment(
        "KAFKA_LISTENERS",
        "PLAINTEXT://0.0.0.0:29092,CONTROLLER://0.0.0.0:9093,EXTERNAL://0.0.0.0:9092"
    )
    .WithEnvironment(
        "KAFKA_ADVERTISED_LISTENERS",
        "PLAINTEXT://kafka:29092,EXTERNAL://localhost:9092"
    )
    .WithEnvironment(
        "KAFKA_LISTENER_SECURITY_PROTOCOL_MAP",
        "PLAINTEXT:PLAINTEXT,CONTROLLER:PLAINTEXT,EXTERNAL:PLAINTEXT"
    )
    .WithEnvironment("KAFKA_CONTROLLER_LISTENER_NAMES", "CONTROLLER")
    .WithEnvironment("KAFKA_INTER_BROKER_LISTENER_NAME", "PLAINTEXT")
    .WithEnvironment("KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS", "0")
    .WithEnvironment("KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR", "1")
    .WithEnvironment("KAFKA_TRANSACTION_STATE_LOG_MIN_ISR", "1")
    .WithEnvironment("KAFKA_LOG_DIRS", "/var/lib/kafka/data")
    .WithEnvironment("KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS", "0")
    .WithEnvironment("KAFKA_BROKER_HEARTBEAT_INTERVAL_MS", "5000")
    .WithEnvironment("KAFKA_BROKER_SESSION_TIMEOUT_MS", "30000")
    .WithEnvironment("KAFKA_CONTROLLER_SOCKET_TIMEOUT_MS", "60000")
    .WithEndpoint(9092, 9092, name: "external")
    .WithEndpoint(9093, 9093, name: "controller")
    .WithEndpoint(29092, 29092, name: "broker")
    .WithTopics(KafkaTopics.GetAllTopics(), numPartitions: 3, replicationFactor: 1)
    .WithVolume("kafka_data", "/var/lib/kafka/data")
    .WithVolume("kafka_metadata", "/var/lib/kafka/metadata");

var schemaRegistry = builder
    .AddContainer("schema-registry", "confluentinc/cp-schema-registry")
    .WithEnvironment("SCHEMA_REGISTRY_HOST_NAME", "schema-registry")
    .WithEnvironment("SCHEMA_REGISTRY_KAFKASTORE_SECURITY_PROTOCOL", "PLAINTEXT")
    .WithEnvironment("SCHEMA_REGISTRY_KAFKASTORE_BOOTSTRAP_SERVERS", "PLAINTEXT://localhost:29092")
    .WithEnvironment("SCHEMA_REGISTRY_LISTENERS", "http://0.0.0.0:8081")
    .WithEndpoint(8081, 8081, "schema-registry")
    .WithReference(kafka.GetEndpoint("broker"))
    .WaitFor(kafka);

var controlCenter = builder
    .AddContainer("control-center", "docker.io/confluentinc/cp-enterprise-control-center")
    .WithEnvironment("CONTROL_CENTER_BOOTSTRAP_SERVERS", "kafka:29092")
    .WithEnvironment("CONTROL_CENTER_REPLICATION_FACTOR", "1")
    .WithEnvironment("CONTROL_CENTER_INTERNAL_TOPICS_PARTITIONS", "1")
    .WithEnvironment("CONTROL_CENTER_MONITORING_INTERCEPTOR_TOPIC_PARTITIONS", "1")
    .WithEnvironment("CONFLUENT_METRICS_TOPIC_REPLICATION", "1")
    .WithEnvironment("CONFLUENT_CONTROLCENTER_INTERNAL_TOPICS_REPLICATION", "1")
    .WithEnvironment("PORT", "9021")
    .WithEndpoint(9021, 9021, name: "control-center")
    .WithReference(kafka.GetEndpoint("external"))
    .WithReference(kafka.GetEndpoint("broker"))
    .WaitFor(kafka);
;

// Web API
builder
    .AddProject<Projects.Books_API>("books-api")
    .WithReference(kafka.GetEndpoint("broker"))
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

// RabbitMQ Consumers
builder
    .AddProject<Projects.Books_RabbitMq_CreatedPublisherConsumer>(
        "rabbit-mq-created-publisher-consumer"
    )
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);
builder
    .AddProject<Projects.Books_RabbitMq_CreatedBookConsumer>("rabbit-mq-created-book-consumer")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);
builder
    .AddProject<Projects.Books_RabbitMq_CreatedAuthorConsumer>("rabbit-mq-created-author-consumer")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.Build().Run();
