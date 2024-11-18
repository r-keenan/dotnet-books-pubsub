using System.Text.Json;
using Avro.Generic;
using Books.API.Services;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Options;

namespace Books.API;

public class KafkaProducerService : IKafkaProducerService, IDisposable
{
    private readonly IProducer<string, GenericRecord> _producer;
    private readonly ISchemaRegistryClient _schemaRegistry;
    private readonly ILogger<KafkaProducerService> _logger;

    public KafkaProducerService(
        IOptions<KafkaProducerConfig> config,
        ILogger<KafkaProducerService> logger
    )
    {
        if (config == null)
            throw new ArgumentNullException(nameof(config));

        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = config.Value.BootstrapServers,
            ClientId = config.Value.ClientId,
        };

        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = config.Value.SchemaRegistryUrl,
        };

        _schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
        _producer = new ProducerBuilder<string, GenericRecord>(producerConfig)
            .SetValueSerializer(new AvroSerializer<GenericRecord>(_schemaRegistry))
            .Build();
        _logger = logger;
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        try
        {
            // Convert message to a GenericRecord using Avro schema
            var schema = await _schemaRegistry.GetLatestSchemaAsync($"{topic}-value");
            var avroSchema = (Avro.RecordSchema)Avro.Schema.Parse(schema.SchemaString);
            var genericRecord = new GenericRecord(avroSchema);
            var jsonString = JsonSerializer.Serialize(message);
            var avroObj = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

            foreach (var prop in avroObj)
            {
                genericRecord.Add(prop.Key, prop.Value);
            }

            var kafkaMessage = new Message<string, GenericRecord>
            {
                Key = Guid.NewGuid().ToString(),
                Value = genericRecord,
            };

            var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);
            _logger.LogInformation(
                $"Message deliver to topic {topic} at partition {deliveryResult.Partition} with offset {deliveryResult.Offset}"
            );
        }
        catch (Exception e)
        {
            _logger.LogError($"Error producing message to topic {topic}: {e.Message}");
            throw;
        }
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
}
