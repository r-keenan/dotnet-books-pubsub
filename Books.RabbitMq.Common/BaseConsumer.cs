using MassTransit;
using Newtonsoft.Json;

namespace Books.RabbitMq.Common
{
    public abstract class BaseConsumer<TMessage> : IConsumer<TMessage>
        where TMessage : class
    {
        public virtual Task Consume(ConsumeContext<TMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            WriteLine($"{typeof(TMessage).Name} message received: {jsonMessage}");

            return ProcessMessage(context);
        }

        protected abstract Task ProcessMessage(ConsumeContext<TMessage> context);
    }
}
