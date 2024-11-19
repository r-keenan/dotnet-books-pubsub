using System.Text.Json;
using Avro;
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

        var avroSerializerConfig = new AvroSerializerConfig
        {
            AutoRegisterSchemas = true,
            SubjectNameStrategy = SubjectNameStrategy.Topic,
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
            var avroSchema = (RecordSchema)
                Avro.Schema.Parse(JsonSerializer.Serialize(new AvroSchema<T>()));
            var subject = $"{topic}-value";
            try
            {
                await _schemaRegistry.GetLatestSchemaAsync(subject);
            }
            catch (SchemaRegistryException)
            {
                var schemaString = avroSchema.ToString();
                await _schemaRegistry.RegisterSchemaAsync(
                    subject,
                    new Confluent.SchemaRegistry.Schema(schemaString, SchemaType.Avro)
                );
            }
            var genericRecord = new GenericRecord(avroSchema);
            var jsonString = JsonSerializer.Serialize(message);
            var avroObj = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

            foreach (var prop in avroObj)
            {
                var value = prop.Value is JsonElement element
                    ? element.ToString()
                    : prop.Value?.ToString();
                genericRecord.Add(prop.Key, value);
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

public class AvroSchema<T>
{
    public string type => "record";
    public string name => typeof(T).Name;
    public Field[] fields =>
        typeof(T)
            .GetProperties()
            .Select(p => new Field { name = p.Name, type = "string" })
            .ToArray();

    public class Field
    {
        public string name { get; set; }
        public string type { get; set; }
    }
}
