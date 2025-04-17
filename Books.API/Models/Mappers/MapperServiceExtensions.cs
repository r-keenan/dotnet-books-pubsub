using Books.API.Models.Mappers.Interfaces;

namespace Books.API.Models.Mappers;

public static class MapperServiceExtensions
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorMapper, AuthorMapper>();
        services.AddSingleton<IBookMapper, BookMapper>();
        services.AddSingleton<IPublisherMapper, PublisherMapper>();
        return services;
    }
}
