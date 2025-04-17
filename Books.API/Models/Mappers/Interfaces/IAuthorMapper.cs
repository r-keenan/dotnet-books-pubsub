namespace Books.API.Models.Mappers.Interfaces;

public interface IAuthorMapper
{
    Author ToEntity(AuthorDto dto);
    AuthorDto ToDto(Author entity);
}
