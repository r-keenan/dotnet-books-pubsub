using Books.RabbitMq.Common;
using Books.Shared.Messages;
using MassTransit;

namespace Books.RabbitMq.CreatedBookConsumer
{
    public class BookCreatedConsumer : BaseConsumer<BookMessage>
    {
        protected override Task ProcessMessage(ConsumeContext<BookMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
