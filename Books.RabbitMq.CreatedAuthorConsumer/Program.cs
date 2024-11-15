using Books.RabbitMq.CreatedAuthorConsumer;
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
                x.AddConsumer<AuthorCreatedConsumer>();

                x.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(hostContext.Configuration.GetConnectionString("rabbitmq"));

                        cfg.ReceiveEndpoint(
                            "author-created-event",
                            e => e.ConfigureConsumer<AuthorCreatedConsumer>(context)
                        );
                    }
                );
            });
        }
    );

await builder.Build().RunAsync();
