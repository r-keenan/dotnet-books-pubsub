namespace Books.API.Services;

public interface IPubSubMessagePublisher
{
    Task PublishMessage<T>(T data, string topicName);
}
