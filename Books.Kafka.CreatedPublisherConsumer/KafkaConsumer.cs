using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Options;

namespace Books.Kafka.CreatedPublisherConsumer
{
    public class KafkaConsumer
    {
        private readonly IConsumer<string, GenericRecord> _consumer;
        private readonly string _topic;

        public KafkaConsumer(IOptions<KafkaConsumerConfig> config, string topic)
        {
            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = config.Value.SchemaRegistryUrl,
            };

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.Value.BootstrapServers,
                GroupId = config.Value.ConsumerGroup,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            var deserializer = new AvroDeserializer<GenericRecord>(schemaRegistry);

            _consumer = new ConsumerBuilder<string, GenericRecord>(consumerConfig)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(deserializer.AsSyncOverAsync())
                .Build();

            _topic = topic.ToString() ?? throw new ArgumentNullException(nameof(topic));
        }

        public async Task ConsumeMessages(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(cancellationToken);
                    var record = result.Message.Value;
                    WriteLine($"Record: {record}");
                }
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
            }
            finally
            {
                _consumer.Close();
            }
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }
}
