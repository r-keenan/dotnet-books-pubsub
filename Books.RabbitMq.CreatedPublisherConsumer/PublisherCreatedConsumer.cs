using Books.Shared.Messages;
using MassTransit;
using Newtonsoft.Json;

namespace Books.RabbitMq.CreatedPublisherConsumer
{
    public class PublisherCreatedConsumer : IConsumer<PublisherMessage>
    {
        public async Task Consume(ConsumeContext<PublisherMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            WriteLine($"PublisherCreated message: {jsonMessage}");
        }
    }
}
