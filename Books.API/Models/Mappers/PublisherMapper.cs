using Books.API.Models.Mappers.Interfaces;
using Riok.Mapperly.Abstractions;

namespace Books.API.Models.Mappers;

[Mapper]
public partial class PublisherMapper : IPublisherMapper
{
    public partial Publisher ToEntity(PublisherDto dto);
    public partial PublisherDto ToDto(Publisher entity);
}

