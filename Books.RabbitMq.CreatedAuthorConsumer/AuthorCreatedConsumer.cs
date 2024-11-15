using Books.Shared.Messages;
using MassTransit;
using Newtonsoft.Json;

namespace Books.RabbitMq.CreatedAuthorConsumer
{
    public class AuthorCreatedConsumer : IConsumer<AuthorMessage>
    {
        public Task Consume(ConsumeContext<AuthorMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            WriteLine($"AuthorCreated message: {jsonMessage}");
            return Task.CompletedTask;
        }
    }
}
