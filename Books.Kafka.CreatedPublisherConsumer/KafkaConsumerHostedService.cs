using Microsoft.Extensions.Hosting;

namespace Books.Kafka.CreatedPublisherConsumer
{
    public class KafkaConsumerHostedService : IHostedService
    {
        private readonly KafkaConsumer _consumer;

        public KafkaConsumerHostedService(KafkaConsumer consumer)
        {
            _consumer = consumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _consumer.ConsumeMessages(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
