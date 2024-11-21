using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Books.Kafka.Common
{
    public class KafkaConsumerHostedService : IHostedService
    {
        private readonly KafkaConsumer _consumer;
        private readonly ILogger<KafkaConsumerHostedService> _logger;

        public KafkaConsumerHostedService(
            KafkaConsumer consumer,
            ILogger<KafkaConsumerHostedService> logger
        )
        {
            _consumer = consumer;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await _consumer.ConsumeMessages(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in consumer service: {ex.Message}");
                    await Task.Delay(5000, cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Kafka consumer service");
            _consumer.Dispose();
            await Task.CompletedTask;
        }
    }
}
