using Books.API.Dtos;
using Riok.Mapperly.Abstractions;

namespace Books.API.Models.Mappers.Interfaces;

public interface IBaseMapper
{
    [MapDerivedType(typeof(Author), typeof(AuthorDto))]
    [MapDerivedType(typeof(Book), typeof(BookDto))]
    [MapDerivedType(typeof(Publisher), typeof(PublisherDto))]
    BaseModel ToEntity(BaseDto dto);

    [MapDerivedType(typeof(AuthorDto), typeof(Author))]
    [MapDerivedType(typeof(BookDto), typeof(Book))]
    [MapDerivedType(typeof(PublisherDto), typeof(Publisher))]
    BaseDto ToDto(BaseModel entity);
}

public interface IBaseMapper<TEntity, TDto> where TEntity : BaseModel where TDto : BaseDto
{
    TDto ToDto(BaseModel entity);
    TEntity ToEntity(TDto dto);
}
