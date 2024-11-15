using Books.RabbitMq.CreatedBookConsumer;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

WriteLine("RabbitMQ Message Consumer using MassTransit");

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<BookCreatedConsumer>();

                x.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(hostContext.Configuration.GetConnectionString("rabbitmq"));

                        cfg.ReceiveEndpoint(
                            "book-created-event",
                            e => e.ConfigureConsumer<BookCreatedConsumer>(context)
                        );
                    }
                );
            });
        }
    );

await builder.Build().RunAsync();
