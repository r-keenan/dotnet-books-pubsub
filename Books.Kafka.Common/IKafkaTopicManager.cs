namespace Books.Kafka.Common
{
    public interface IKafkaTopicManager
    {
        Task CreateTopicIfNotExists(
            IEnumerable<string> topicNames,
            int numbPartitions,
            short replicationFactor
        );
    }
}
