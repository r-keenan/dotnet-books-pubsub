namespace Books.API.Models.Mappers.Interfaces;

public interface IBookMapper
{
    Book ToEntity(BookDto dto);
    BookDto ToDto(Book entity);
}
