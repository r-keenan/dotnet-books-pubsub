using Books.API;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class PublisherMapper : IPublisherMapper
{
    public partial Publisher ToEntity(PublisherDto dto);
    public partial PublisherDto ToDto(Publisher entity);
}

