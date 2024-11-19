using Books.Kafka.CreatedPublisherConsumer;
using Books.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

WriteLine("starting consumer...");

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(
        (hostContext, config) =>
        {
            config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();
        }
    )
    .ConfigureServices(
        (context, services) =>
        {
            var kafkaConfig = context.Configuration.GetSection("Kafka").Get<KafkaConsumerConfig>();
            if (kafkaConfig == null)
            {
                throw new ArgumentNullException(nameof(kafkaConfig));
            }

            services.Configure<KafkaConsumerConfig>(context.Configuration.GetSection("Kafka"));
            services.AddSingleton<KafkaConsumer>(sp => new KafkaConsumer(
                sp.GetRequiredService<IOptions<KafkaConsumerConfig>>(),
                KafkaTopics.PublishersTopic
            ));

            services.AddHostedService<KafkaConsumerService>();
        }
    )
    .Build()
    .RunAsync();
