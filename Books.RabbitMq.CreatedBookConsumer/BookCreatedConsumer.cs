using Books.Common.Messages;
using Books.RabbitMq.Common;
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
