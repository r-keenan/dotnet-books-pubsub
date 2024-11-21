using System.Runtime.CompilerServices;
using System.Text.Json;
using Avro;
using Avro.Generic;
using Books.API.Services;
using Books.Common;
using Books.Common.Constants;
using Books.Kafka.Common;
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
    private readonly IKafkaTopicManager _topicManager;

    public async Task InitializeAsync()
    {
        var topics = KafkaTopics.GetAllTopics().ToList();
        _logger.LogInformation($"Initializing topics: {string.Join(", ", topics)}");
        await _topicManager.CreateTopicIfNotExists(topics, 3, 1);
    }

    public KafkaProducerService(
        IOptions<KafkaProducerConfig> config,
        ILogger<KafkaProducerService> logger,
        IKafkaTopicManager topicManager
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
            RequestTimeoutMs = 5000,
            MaxCachedSchemas = 10,
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

        _topicManager = topicManager;
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
                object? value;
                if (prop.Value is JsonElement element)
                {
                    var fieldSchema = avroSchema.Fields.First(f => f.Name == prop.Key).Schema;
                    value = element.ValueKind switch
                    {
                        JsonValueKind.Number => fieldSchema.Tag switch
                        {
                            Avro.Schema.Type.Int => element.TryGetInt32(out var intVal)
                                ? intVal
                                : (int)element.GetDouble(),
                            Avro.Schema.Type.Long => element.TryGetInt64(out var longVal)
                                ? longVal
                                : (long)element.GetDouble(),
                            Avro.Schema.Type.Double => element.GetDouble(),
                            _ => element.ToString(),
                        },
                        JsonValueKind.String => fieldSchema.Tag switch
                        {
                            Avro.Schema.Type.Long
                                when DateTime.TryParse(element.GetString(), out var date) => (
                                (DateTimeOffset)date
                            ).ToUnixTimeMilliseconds(),
                            _ => element.GetString(),
                        },
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => null,
                        _ => element.ToString(),
                    };
                }
                else
                {
                    value = prop.Value;
                }
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
