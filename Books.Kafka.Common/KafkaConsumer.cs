using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Books.Kafka.Common
{
    public class KafkaConsumer
    {
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly IConsumer<string, GenericRecord> _consumer;
        private readonly string _topic;
        private readonly AsyncRetryPolicy _retryPolicy;

        public KafkaConsumer(
            IOptions<KafkaConsumerConfig> config,
            ILogger<KafkaConsumer> logger,
            string topic
        )
        {
            _logger = logger;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            $"Connection attempt {retryCount} failed: {exception.Message}. Retrying in {timeSpan.TotalSeconds} seconds..."
                        );
                    }
                );

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = config.Value.SchemaRegistryUrl,
            };

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.Value.BootstrapServers,
                GroupId = config.Value.ConsumerGroup,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoOffsetStore = false,
                MaxPollIntervalMs = 300000,
                SessionTimeoutMs = 30000,
                HeartbeatIntervalMs = 3000,
                RetryBackoffMs = 1000,
            };

            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            var deserializer = new AvroDeserializer<GenericRecord>(schemaRegistry);

            _consumer = new ConsumerBuilder<string, GenericRecord>(consumerConfig)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(deserializer.AsSyncOverAsync())
                .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}"))
                .SetPartitionsAssignedHandler(
                    (c, p) => _logger.LogInformation($"Assigned partitions: {string.Join(",", p)}")
                )
                .SetPartitionsRevokedHandler(
                    (c, p) => _logger.LogInformation($"Revoked partitions: {string.Join(",", p)}")
                )
                .Build();

            _topic = topic.ToString() ?? throw new ArgumentNullException(nameof(topic));
        }

        public async Task ConsumeMessages(CancellationToken cancellationToken)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                _consumer.Subscribe(_topic);
                _logger.LogInformation($"Successfully connect to Kafka topic: {_topic}");
            });

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = await _retryPolicy.ExecuteAsync(async () =>
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        if (consumeResult != null)
                        {
                            await ProcessMessage(consumeResult);
                            _consumer.Commit(consumeResult);
                        }
                        return consumeResult;
                    });
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Unrecoverable error: {ex.Message}");
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                _consumer?.Close();
                _consumer?.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during disposal: {ex.Message}");
            }
        }

        private async Task ProcessMessage(ConsumeResult<string, GenericRecord> result)
        {
            try
            {
                _logger.LogInformation($"Processing message: {result.Message.Value}");
                // Add your message processing logic here
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
                throw; // Rethrow to trigger retry
            }
        }
    }
}
