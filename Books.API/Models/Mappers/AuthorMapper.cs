using Books.API.Models.Mappers.Interfaces;
using Riok.Mapperly.Abstractions;

namespace Books.API.Models.Mappers;

[Mapper]
public partial class AuthorMapper : IAuthorMapper
{
    public partial AuthorDto ToDto(Author entity);

    public partial Author ToEntity(AuthorDto dto);
}
