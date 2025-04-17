using Books.API;

public interface IPublisherMapper
{
    Publisher ToEntity(PublisherDto dto);
    PublisherDto ToDto(Publisher entity);
}
