namespace Books.API;

public interface IKafkaProducerService
{
    Task ProduceAsync<T>(string topic, T message);
}
