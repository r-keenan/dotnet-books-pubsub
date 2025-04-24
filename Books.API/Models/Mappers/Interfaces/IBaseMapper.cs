using Books.API.Dtos;
using Books.Common.Messages;
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

    [MapDerivedType(typeof(AuthorDto), typeof(AuthorMessage))]
    [MapDerivedType(typeof(BookDto), typeof(BookMessage))]
    [MapDerivedType(typeof(PublisherDto), typeof(PublisherMessage))]
    BaseMessage ToMessage(BaseDto dto);

    [MapDerivedType(typeof(Author), typeof(AuthorMessage))]
    [MapDerivedType(typeof(Book), typeof(BookMessage))]
    [MapDerivedType(typeof(Publisher), typeof(PublisherMessage))]
    BaseMessage ToMessage(BaseModel entity);

    string GetKafkaTopic(BaseModel entity);
}

public interface IBaseMapper<TEntity, TDto, TMessage>
    where TEntity : BaseModel
    where TDto : BaseDto
    where TMessage : BaseMessage
{
    TDto ToDto(BaseModel entity);
    TEntity ToEntity(TDto dto);
    TMessage ToMessage(TDto dto);
    TMessage ToMessage(BaseModel entity);
    string GetKafkaTopic(BaseModel entity);
}
