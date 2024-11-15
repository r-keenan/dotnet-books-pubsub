using Books.RabbitMq.CreatedPublisherConsumer;
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
                x.AddConsumer<PublisherCreatedConsumer>();

                x.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(hostContext.Configuration.GetConnectionString("rabbitmq"));

                        cfg.ReceiveEndpoint(
                            "publisher-created-event",
                            e => e.ConfigureConsumer<PublisherCreatedConsumer>(context)
                        );
                    }
                );
            });
        }
    );

await builder.Build().RunAsync();
