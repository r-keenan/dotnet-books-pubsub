using Microsoft.Extensions.Hosting;

namespace Books.Kafka.Common
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly KafkaConsumer _consumer;

        public KafkaConsumerService(KafkaConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ConsumeMessages(stoppingToken);
        }
    }
}
