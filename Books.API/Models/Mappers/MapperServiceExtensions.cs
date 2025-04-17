using Books.API;

public static class MapperServiceExtensions
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IPublisherMapper, PublisherMapper>();
        return services;
    }
}
