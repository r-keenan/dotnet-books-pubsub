using Books.RabbitMq.Common;
using Books.Shared.Messages;
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
