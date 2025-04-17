using Books.API.Dtos;
using Books.API.Models.Mappers.Interfaces;
using Riok.Mapperly.Abstractions;

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

}

