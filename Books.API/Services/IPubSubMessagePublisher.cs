namespace Books.API;

public interface IPubSubMessagePublisher
{
    Task PublishMessage<T>(T data, string topicName);
}
