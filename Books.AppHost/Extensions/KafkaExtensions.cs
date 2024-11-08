using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Books.AppHost.Extensions;

public static class KafkaExtensions
{
    public static IResourceBuilder<ContainerResource> WithTopics(this IResourceBuilder<ContainerResource> kafka,
        IEnumerable<string> topics, int numPartitions = 1, short replicationFactor = 1)
    {
        kafka.WithAnnotation(new KafkaTopicAnnotation(topics, numPartitions, replicationFactor));
        return kafka;
    }
}

public class KafkaTopicAnnotation : IResourceAnnotation
{
    private readonly IEnumerable<string> _topics;
    private readonly int _numPartitions;
    private readonly short _replicationFactor;

    public KafkaTopicAnnotation(IEnumerable<string> topics, int numPartitions, short replicationFactor)
    {
        _topics = topics;
        _numPartitions = numPartitions;
        _replicationFactor = replicationFactor;
    }

    public async Task ConfigureAsync(IResourceBuilder<ContainerResource> builder, CancellationToken cancellationToken)
    {
        var resource = (builder as IResourceBuilder<ContainerResource>)?.Resource
            ?? throw new InvalidOperationException("Resource is not a container");
        EndpointReference endpoint = resource.GetEndpoint("kafka");
        var bootstrapServers = $"{endpoint.Host}:${endpoint.Port}";

        // Wait for Kafka to be ready
        await WaitForKafkaAsync(bootstrapServers, cancellationToken);

        // Create topics
        var adminConfig = new AdminClientConfig
        {
            BootstrapServers = bootstrapServers
        };

        using var adminClient = new AdminClientBuilder(adminConfig).Build();

        var topicSpecs = _topics.Select(topic => new TopicSpecification
        {
            Name = topic,
            NumPartitions = _numPartitions,
            ReplicationFactor = _replicationFactor
        });

        try
        {
            await adminClient.CreateTopicsAsync(topicSpecs);
            Console.WriteLine("Successfully created topics: " + string.Join(", ", _topics));
        }
        catch (CreateTopicsException ex)
        {
            Console.WriteLine($"Error creating topics: {ex.Message}");
            // Don't throw - topics might already exist
        }
    }

    private static async Task WaitForKafkaAsync(string bootstrapServers, CancellationToken token)
    {
        var config = new AdminClientConfig
        {
            BootstrapServers = bootstrapServers
        };

        using var adminClient = new AdminClientBuilder(config).Build();
        var attempts = 0;
        const int maxAttempts = 30;

        while (attempts < maxAttempts && !token.IsCancellationRequested)
        {
            try
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
                if (metadata.Brokers.Count > 0)
                {
                    Console.WriteLine("Successfully connected to Kafka");
                    return;
                }
            }
            catch
            {
                attempts++;
                Console.WriteLine($"Waiting for Kafka to be ready... Attempt {attempts}/{maxAttempts}");
                await Task.Delay(2000, token);
            }
        }

        if (attempts >= maxAttempts)
        {
            throw new Exception("Failed to connect to Kafka after maximum attempts");
        }
    }
}
