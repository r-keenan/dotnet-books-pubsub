namespace Books.API.Models.Mappers.Interfaces;

public interface IPublisherMapper
{
    Publisher ToEntity(PublisherDto dto);
    PublisherDto ToDto(Publisher entity);
}
