using Books.Common.Messages;
using Books.RabbitMq.Common;
using MassTransit;

namespace Books.RabbitMq.CreatedPublisherConsumer
{
    public class PublisherCreatedConsumer : BaseConsumer<PublisherMessage>
    {
        protected override Task ProcessMessage(ConsumeContext<PublisherMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
