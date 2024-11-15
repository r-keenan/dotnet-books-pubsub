using Books.Shared.Messages;
using MassTransit;
using Newtonsoft.Json;

namespace Books.RabbitMq.CreatedBookConsumer
{
    public class BookCreatedConsumer : IConsumer<BookMessage>
    {
        public Task Consume(ConsumeContext<BookMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            WriteLine($"BookCreated message: {jsonMessage}");
            return Task.CompletedTask;
        }
    }
}
