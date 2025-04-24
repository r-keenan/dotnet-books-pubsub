using Books.API.Dtos;
using Books.API.Models.Mappers.Interfaces;
using Books.Common.Messages;
using Riok.Mapperly.Abstractions;
using static Books.Common.Enums.Kafka.Kafka;

namespace Books.API.Models.Mappers;

[Mapper]
public partial class BaseMapper : IBaseMapper
{

    // Base mapping method that handles polymorphism
    [MapDerivedType(typeof(Author), typeof(AuthorDto))]
    [MapDerivedType(typeof(Book), typeof(BookDto))]
    [MapDerivedType(typeof(Publisher), typeof(PublisherDto))]
    public partial BaseDto ToDto(BaseModel entity);

    [MapDerivedType(typeof(AuthorDto), typeof(Author))]
    [MapDerivedType(typeof(BookDto), typeof(Book))]
    [MapDerivedType(typeof(PublisherDto), typeof(Publisher))]
    public partial BaseModel ToEntity(BaseDto dto);

    [MapDerivedType(typeof(AuthorDto), typeof(AuthorMessage))]
    [MapDerivedType(typeof(BookDto), typeof(BookMessage))]
    [MapDerivedType(typeof(PublisherDto), typeof(PublisherMessage))]
    public partial BaseMessage ToMessage(BaseDto dto);

    [MapDerivedType(typeof(Author), typeof(AuthorMessage))]
    [MapDerivedType(typeof(Book), typeof(BookMessage))]
    [MapDerivedType(typeof(Publisher), typeof(PublisherMessage))]
    public partial BaseMessage ToMessage(BaseModel entity);


    public string GetKafkaTopic(BaseModel entity)
    {
        if (entity is Author) return Topic.Authors.ToString();
        if (entity is Book) return Topic.Books.ToString();
        if (entity is Publisher) return Topic.Publishers.ToString();
        return Topic.Default.ToString();
    }
}

