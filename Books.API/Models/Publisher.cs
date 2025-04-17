using Books.Common.Enums;
using Riok.Mapperly.Abstractions;

namespace Books.API;

public class Publisher : BaseModel
{
    public string Name { get; set; } = "";
    public string Address1 { get; set; } = "";
    public string Address2 { get; set; } = "";
    public string City { get; set; } = "";
    public State State { get; set; }
    public string ZipCode { get; set; } = "";
    public DateOnly DateFounded { get; set; }

    // Need for Entity Framework Core migrations
    public Publisher() { }

}
public interface IPublisherMapper
{
    Publisher ToEntity(PublisherDto dto);
    PublisherDto ToDto(Publisher entity);
}

[Mapper]
public partial class PublisherMapper : IPublisherMapper
{
    public partial Publisher ToEntity(PublisherDto dto);
    public partial PublisherDto ToDto(Publisher entity);
}

public static class MapperServiceExtensions
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IPublisherMapper, PublisherMapper>();
        return services;
    }
}
