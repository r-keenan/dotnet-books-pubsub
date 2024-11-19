namespace Books.Kafka.CreatedPublisherConsumer
{
    public class KafkaConsumerConfig
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string ConsumerGroup { get; set; } = string.Empty;
        public string SchemaRegistryUrl { get; set; } = string.Empty;
    }
}
