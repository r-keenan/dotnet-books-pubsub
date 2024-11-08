using Books.API.Services;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Books.API;

public class KafkaProducerService: IKafkaProducerService, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducerService> _logger;

    public KafkaProducerService(IOptions<KafkaProducerConfig> config, ILogger<KafkaProducerService> logger)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));

        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = config.Value.BootstrapServers,
            ClientId = config.Value.ClientId
        };

        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
        _logger = logger;
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        try
        {
            // TODO: Serialize this as Avro instead of JSON for better performance
            var jsonMessage = JsonSerializer.Serialize(message);
            var kafkaMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = jsonMessage
            };

            var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);
            _logger.LogInformation($"Message deliver to topic {topic} at partition {deliveryResult.Partition} with offset {deliveryResult.Offset}");

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
