using Books.API;
using Books.API.Constants;
using Books.API.Models;
using Books.API.Models.Validators;
using Books.API.Repositories;
using Books.API.Services;
using Books.Kafka.Common;
using Books.ServiceDefaults.Hosting;
using Confluent.Kafka;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();

// This is referencing the books container of Postgres in .Net Aspire
var connectionString = builder.Configuration.GetConnectionString("postgres-books");
builder.Services.AddDbContext<BooksDbContext>(opt =>
{
    opt.UseNpgsql(connectionString);
});
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new QueryStringApiVersionReader("api-version")
    );
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq(
        (context, cfg) =>
        {
            var rabbitConfig = builder.Configuration.GetConnectionString("rabbitmq");
            cfg.Host(rabbitConfig);

            cfg.UseMessageRetry(r =>
                r.Intervals(
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                )
            );
            cfg.ConfigureEndpoints(context);
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books API", Version = "v1" });
});
builder
    .Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<AuthorValidator>()
    .AddValidatorsFromAssemblyContaining<PublisherValidator>()
    .AddValidatorsFromAssemblyContaining<BookValidator>();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAdminClient>(sp =>
{
    var bootstrapServers = builder.Configuration.GetValue<string>("Kafka:BootstrapServers");
    var adminConfig = new AdminClientConfig
    {
        BootstrapServers = bootstrapServers,
        SecurityProtocol = SecurityProtocol.Plaintext,
        ApiVersionRequest = true,
        SocketTimeoutMs = 10000,
        ConnectionsMaxIdleMs = 180000,
    };
    var logger = sp.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Configuring Kafka with bootstrap servers: {bootstrapServers}");

    return new AdminClientBuilder(adminConfig).Build();
});

builder.Services.AddSingleton<IKafkaTopicManager, KafkaTopicManager>();
builder.Services.Configure<KafkaProducerConfig>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddSingleton<ApiEndpoints>();

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IPublisherRepository, PublisherRepository>();
builder.Services.AddTransient(typeof(IHttpApiRepository<>), typeof(HttpApiRepository<>));

var app = builder.Build();

app.UseExceptionHandler();

app.MapDefaultEndpoints();
// Initialize Kafka topics
using (var scope = app.Services.CreateScope())
{
    var kafkaProducer = scope.ServiceProvider.GetRequiredService<IKafkaProducerService>();
    await ((KafkaProducerService)kafkaProducer).InitializeAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
