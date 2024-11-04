using Books.API.Constants;
using Books.API.Models;
using Books.API.Repositories;
using Books.API.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("BooksDatabaseConnection");
builder.Services.AddDbContext<BooksDbContext>(opt =>
{
    opt.UseNpgsql(connectionString);
});
builder.Services.AddControllers();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ApiEndpoints>();

builder.Services.AddScoped<IPubSubMessagePublisher, PubSubMessagePublisher>();

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IPublisherRepository, PublisherRepository>();
builder.Services.AddTransient(typeof(IHttpApiRepository<>), typeof(HttpApiRepository<>));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
