using Books.Common.Messages;
using Books.RabbitMq.Common;
using MassTransit;

namespace Books.RabbitMq.CreatedAuthorConsumer
{
    public class AuthorCreatedConsumer : BaseConsumer<AuthorMessage>
    {
        protected override Task ProcessMessage(ConsumeContext<AuthorMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
