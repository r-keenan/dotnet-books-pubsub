using Books.API.Models.Mappers.Interfaces;
using Riok.Mapperly.Abstractions;

namespace Books.API.Models.Mappers;

[Mapper]
public partial class BookMapper : IBookMapper
{
    public partial BookDto ToDto(Book entity);

    public partial Book ToEntity(BookDto dto);
}
