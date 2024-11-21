using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Books.Kafka.Common
{
    public class KafkaTopicManager : IKafkaTopicManager
    {
        private readonly IAdminClient _adminClient;
        private readonly ILogger<KafkaTopicManager> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public KafkaTopicManager(IAdminClient adminClient, ILogger<KafkaTopicManager> logger)
        {
            _adminClient = adminClient;
            _logger = logger;

            _retryPolicy = Policy
                .Handle<KafkaException>()
                .WaitAndRetryAsync(
                    7,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            $"Attempt {retryCount} to connect to Kafka failed. Retrying in {timeSpan.TotalSeconds} seconds. Error: {exception.Message}"
                        );
                    }
                );
        }

        public async Task CreateTopicIfNotExists(
            IEnumerable<string> topicNames,
            int numbPartitions = 1,
            short replicationFactor = 1
        )
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    _logger.LogInformation(
                        $"Attempting to create topics: {string.Join(", ", topicNames)}"
                    );

                    // Get existing topics
                    var metadata = _adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    var existingTopics = metadata.Topics.Select(t => t.Topic).ToHashSet();

                    var topicsToCreate = topicNames
                        .Where(topicName => !existingTopics.Contains(topicName))
                        .Select(topicName => new TopicSpecification
                        {
                            Name = topicName,
                            NumPartitions = numbPartitions,
                            ReplicationFactor = replicationFactor,
                        })
                        .ToList();

                    if (topicsToCreate.Any())
                    {
                        _logger.LogInformation(
                            $"Creating topics: {string.Join(", ", topicsToCreate.Select(t => t.Name))}"
                        );

                        await _adminClient.CreateTopicsAsync(topicsToCreate);
                        _logger.LogInformation(
                            $"Topics created successfully: {string.Join(", ", topicsToCreate.Select(t => t.Name))}"
                        );
                        _logger.LogInformation(
                            $"Topics created successfully: {string.Join(", ", topicsToCreate.Select(t => t.Name))}"
                        );
                    }
                    else
                    {
                        _logger.LogInformation("All topics already exist");
                    }
                }
                catch (CreateTopicsException e)
                {
                    _logger.LogError(
                        $"An error occurred creating topics: {e.Results[0].Error.Reason}"
                    );
                    throw;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Unexpected error creating topics: {e.Message}");
                    throw;
                }
            });
        }
    }
}
